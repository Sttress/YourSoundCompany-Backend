
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourSoundCompany.Business;
using YourSoundCompany.Business.Model.User.DTO;
using YourSoundCompany.Business.Validation.User;
using YourSoundCompany.CacheService.Service;
using YourSoundCompany.EmailService;
using YourSoundCompany.Templates;
using YourSoundCompany.Templates.Enum;
using YourSoundCompnay.Business.Model;
using YourSoundCompnay.Business.Model.User;
using YourSoundCompnay.Business.Validation.User;
using YourSoundCompnay.RelationalData;
using YourSoundCompnay.RelationalData.Entities;
using YourSoundCompnay.SesseionService;
using C = BCrypt.Net;


namespace YourSoundCompnay.Business.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserCreateValidator _userCreateValidator;
        private readonly UserUpdateValidator _userUpdateValidator;
        private readonly UserRecoveryPasswordValidator _userRecoveryPasswordValidator;
        private readonly ISessionService _sessionService;
        private readonly ICacheService _cacheService;
        private readonly ISendEmailService _sendEmailService;
        private readonly IUtilsService _utilsService;

        public UserService
            (
                IUserRepository userRepositry,
                IMapper mapper,
                UserUpdateValidator userUpdateValidator,
                UserCreateValidator userCreateValidator,
                UserRecoveryPasswordValidator userRecoveryPasswordValidator,
                ISessionService sessionService,
                ICacheService cacheService,
                ISendEmailService sendEmailService,
                IUtilsService utilsService
            )
        {
            _userRepository = userRepositry;
            _mapper = mapper;
            _userUpdateValidator = userUpdateValidator;
            _userCreateValidator = userCreateValidator;
            _sessionService = sessionService;
            _cacheService = cacheService;
            _sendEmailService = sendEmailService;
            _utilsService = utilsService;
            _userRecoveryPasswordValidator = userRecoveryPasswordValidator;
        }

        private string _Key_EmailCode(string email) => $"VerificationCode_{email}";
        private string _Key_RecoveryPassword(string email) => $"RecoveryPasswordCode_{email}";

        public async Task<BaseResponse<UserResponseDTO>> Update(UserUpdateDTO model)
        {
            try
            {
                var result = new BaseResponse<UserResponseDTO>();
                var entity = new UserEntity();

                if (model is not null)
                {
                    result.Message = result.Validate(await _userUpdateValidator.ValidateAsync(model));

                }
                else
                {
                    result.Message.Add("As informações do usuário não poderam ser processadas!");
                    return result;
                }

                entity = await _userRepository.GetById(model.Id);

                if(entity is null)
                {
                    result.Message.Add("Usuário não foi encontrado para realizar a alteração!");
                    return result;
                }

                entity.Name = model.Name;
                entity.UrlImageProfile = model.UrlImageProfile;

                entity = await _userRepository.Update(entity);

                result.Data = _mapper.Map<UserResponseDTO>(entity);

                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<BaseResponse<UserResponseDTO>> Create(UserCreateDTO model)
        {
            try
            {
                var result = new BaseResponse<UserResponseDTO>();
                var entity = new UserEntity();

                if(model is not null)
                {
                    result.Message = result.Validate(await _userCreateValidator.ValidateAsync(model));

                }
                else
                {
                    result.Message.Add("As informações do usuário não poderam ser processadas!");
                    return result;
                }

                if (result.Message.Count() > 0)
                {
                    return result;
                }

                var codeVerify =  _utilsService.CreateRandomCodeInt();

                await _cacheService.Set(this._Key_EmailCode(model.Email), codeVerify, TimeSpan.FromMinutes(60));

                await _sendEmailService.VerificationEmail(model.Email,model.Name,codeVerify);

                entity = new UserEntity()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = C.BCrypt.HashPassword(model.Password),
                    Active = false
                };



                entity = await _userRepository.Create(entity);

                result.Data = _mapper.Map<UserResponseDTO>(entity);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<BaseResponse<UserResponseDTO>> VerifyEmailCode(UserVerificationEmailDTO model)
        {
            try
            {
                var result = new BaseResponse<UserResponseDTO>();

                if (string.IsNullOrEmpty(model.Email))
                {
                    result.Message.Add("O email não pode ser identificado!");
                    return result;
                }

                var key = this._Key_EmailCode(model.Email);

                var codeCache = await _cacheService.Get<int>(key);
                if(model.Code == null)
                {
                    result.Message.Add("O código de verificação é obrigatório!");
                    return result;
                }

                if(model.Code != codeCache)
                {
                    result.Message.Add("O código de verificação fornecido não foi o mesmo enviado por email para a verificação!");
                    return result;
                }

                var userDB = (await _userRepository.GetUserByEmail(model.Email)).FirstOrDefault();
                
                if(userDB is not null)
                {
                    userDB.Active = true;
                    await _userRepository.SaveChanges();
                }

                await _cacheService.Remove(key);

                var user = _mapper.Map<UserResponseDTO>(userDB);
                result.Data = user;
                return result;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<UserResponseDTO?> GetCurrentUser()
        {
            try
            {
                var userId = _sessionService.UserId;

                if (userId > 0)
                {
                    var user = (await GetById(userId)).Data;
                    return _mapper.Map<UserResponseDTO>(user);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseResponse<UserResponseDTO>> GetById(long? id)
        {
            try
            {
                var result = new BaseResponse<UserResponseDTO>();

                var user = await _userRepository.GetById(id);
                if (user == null)
                {
                    result.Message.Add("Usuário não pode ser encontrado");
                    return result;

                }

                result.Data = _mapper.Map<UserResponseDTO>(user);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<UserModel>> GetByEmail(string email)
        {
            return _mapper.Map<List<UserModel>>(await _userRepository.GetUserByEmail(email));
        }

        public async Task<BaseResponse<UserResponseDTO>> RecoveryPassword(string email)
        {
            try
            {
                var result = new BaseResponse<UserResponseDTO>();

                if (string.IsNullOrEmpty(email))
                {
                    result.Message.Add("Email não é valido");
                    return result;
                }

                var user = (await GetByEmail(email)).Where(e => e.Active).FirstOrDefault();

                var code = _utilsService.GenerateRandomString();
                await _cacheService.Set(this._Key_RecoveryPassword(user.Email), code, TimeSpan.FromMinutes(60));

                await _sendEmailService.RecoveryPasswordEmail(user.Email, user.Name, code);

                result.Data = _mapper.Map<UserResponseDTO>(user);

                return result;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseResponse<UserResponseDTO>> RecoveryPasswordVerified(UserRecoveryPasswordVerifiedDTO model)
        {
            try
            {
                var result = new BaseResponse<UserResponseDTO>();

                result.Message = result.Validate(await _userRecoveryPasswordValidator.ValidateAsync(model));

                if (result.Message.Any())
                {
                    return result;
                }

                var key = this._Key_RecoveryPassword(model.Email);

                var code = await _cacheService.Get<string>(key);

                if(!code.Equals(model.Code))
                {
                    result.Message.Add("Não foi possivel validar a identidade do usúario!");
                    return result;
                }

                var user = (await _userRepository.GetUserByEmail(model.Email)).Where(e => e.Active).FirstOrDefault(); 
                if (user is not null)
                {
                    user.Password = model.Password;
                    await _userRepository.Update(user);
                }

                await _cacheService.Remove(key);

                result.Data = _mapper.Map<UserResponseDTO>(user);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}


using AutoMapper;
using Microsoft.AspNetCore.Http;
using YourSoundCompany.Business.Model.User;
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
        private readonly ISessionService _sessionService;
        private readonly IEmailService _emailService;
        private readonly ITemplateEmailService _templateEmailService;
        private readonly ICacheService _cacheService;

        public UserService
            (
                IUserRepository userRepositry,
                IMapper mapper,
                UserUpdateValidator userUpdateValidator,
                UserCreateValidator userCreateValidator,
                ISessionService sessionService,
                IEmailService emailService,
                ITemplateEmailService templateEmailService,
                ICacheService cacheService
            )
        {
            _userRepository = userRepositry;
            _mapper = mapper;
            _userUpdateValidator = userUpdateValidator;
            _userCreateValidator = userCreateValidator;
            _sessionService = sessionService;
            _emailService = emailService;
            _templateEmailService = templateEmailService;
            _cacheService = cacheService;
        }

        private string _Key_EmailCode(string email) => $"VerificationCode_{email}";

        public Task<BaseResponse<UserResponseModel>> Update(UserCreateModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<UserResponseModel>> Create(UserCreateModel model)
        {
            try
            {
                var result = new BaseResponse<UserResponseModel>();
                var validation = new List<string>();
                var entity = new UserEntity();


                if (model.Id > 0)
                {
                    validation = result.Validate(await _userUpdateValidator.ValidateAsync(model));
                }
                else
                {
                    validation = result.Validate(await _userCreateValidator.ValidateAsync(model));
                }



                if (validation.Count() > 0)
                {
                    result.Message = validation;
                    return result;
                }

                entity = model.Id > 0 ? await _userRepository.GetById(model.Id) : null;

                if (entity != null)
                {
                    entity.Name = model.Name;
                    entity.Email = model.Email;
                    await _userRepository.Update(entity);
                }
                else
                {
                    entity = new UserEntity()
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Password = C.BCrypt.HashPassword(model.Password),
                        Active = false
                    };

                    await SendEmailVerification(model.Email, model.Name);

                    entity = await _userRepository.Create(entity);

                }


                await _userRepository.SaveChanges();

                result.Data = _mapper.Map<UserResponseModel>(entity);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async Task<int> CreateRandomCode(string emailUser)
        {
            var random = new Random();
            var code = random.Next(100000, 999999);

            await _cacheService.Set(this._Key_EmailCode(emailUser), code, TimeSpan.FromMinutes(5));

            return code;
        }

        private async Task SendEmailVerification(string email, string name)
        {
            try
            {
                var codeVerify = await CreateRandomCode(email);

                object obj = new
                {
                    userName = name,
                    code = codeVerify
                };

                var body = await _templateEmailService.RenderTemplate(TemplateEmailEnum.VerifyEmail, obj);
                await _emailService.SendEmailAsync(email, "Código de Verificação", body);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseResponse<UserResponseModel>> VerifyEmailCode(UserVerificationEmail model)
        {
            try
            {
                var codeCache = await _cacheService.Get<int>(this._Key_EmailCode(model.Email));
                var result = new BaseResponse<UserResponseModel>();
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

                var user = _mapper.Map<UserResponseModel>(userDB);
                result.Data = user;
                return result;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<UserResponseModel?> GetCurrentUser()
        {
            try
            {
                var userId = _sessionService.UserId;

                if (userId > 0)
                {
                    var user = (await GetById(userId)).Data;
                    return _mapper.Map<UserResponseModel>(user);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseResponse<UserResponseModel>> GetById(long? id)
        {
            try
            {
                var result = new BaseResponse<UserResponseModel>();

                var user = await _userRepository.GetById(id);
                if (user == null)
                {
                    result.Message.Add("Usuário não existe");
                    return result;

                }

                result.Data = _mapper.Map<UserResponseModel>(user);

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

        public async Task<bool> VerifyUserById(long id)
        {
            try
            {

                var user = await _userRepository.GetById(id);
                if (user == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

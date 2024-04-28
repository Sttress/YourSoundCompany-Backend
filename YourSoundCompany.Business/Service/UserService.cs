
using AutoMapper;
using System.Text.RegularExpressions;
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

        public UserService
            (
                IUserRepository userRepositry,
                IMapper mapper,
                UserUpdateValidator userUpdateValidator,
                UserCreateValidator userCreateValidator,
                ISessionService sessionService
            )
        {
            _userRepository = userRepositry;
            _mapper = mapper;
            _userUpdateValidator = userUpdateValidator;
            _userCreateValidator = userCreateValidator;
            _sessionService = sessionService;
        }

        public async Task<BaseResponse<UserResponseModel>> CreateUpdate(UserCreateModel model)
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

                entity = model.Id > 0 ? await _userRepository.GetUserById(model.Id) : null;

                if (entity != null)
                {
                    entity.Name = model.Name;
                    entity.Email = model.Email;
                }
                else
                {
                    entity = new UserEntity()
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Password = C.BCrypt.HashPassword(model.Password)
                    };
                    entity = (await _userRepository.GetDbSetUser().AddAsync(entity)).Entity;

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

                var user = await _userRepository.GetUserById(id);
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

        public async Task<bool> VerifyUserById(long id)
        {
            try
            {

                var user = await _userRepository.GetUserById(id);
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

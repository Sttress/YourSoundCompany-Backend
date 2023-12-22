
using AutoMapper;
using System.Text.RegularExpressions;
using SystemStock.Business.Model;
using SystemStock.Business.Model.User;
using SystemStock.Business.Validation.User;
using SystemStock.RelationalData;
using SystemStock.RelationalData.Entities;
using C = BCrypt.Net;


namespace SystemStock.Business.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserCreateValidator _userCreateValidator;
        private readonly UserUpdateValidator _userUpdateValidator;

        public UserService
            (
                IUserRepository userRepositry, 
                IMapper mapper, 
                UserUpdateValidator userUpdateValidator,
                UserCreateValidator userCreateValidator
            )
        {
            _userRepository = userRepositry;
            _mapper = mapper;
            _userUpdateValidator = userUpdateValidator;
            _userCreateValidator = userCreateValidator;
        }

        public async Task<BaseResponse<UserResponseModel>> CreateUpdate(UserCreateModel model)
        {
            try
            {
                var result = new BaseResponse<UserResponseModel>();
                var validation = new List<string>();
                var entity = new UserEntity();

                if(model.Id > 0)
                {
                    validation = result.Validate(await _userUpdateValidator.ValidateAsync(model));
                }
                else
                {
                    validation = result.Validate(await _userCreateValidator.ValidateAsync(model));
                }

                if(validation.Count() > 0)
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

        public async Task<BaseResponse<UserResponseModel>> GetById(long id)
        {
            try
            {
                var result = new BaseResponse<UserResponseModel>();

                var user = await _userRepository.GetUserById(id);
                if(user == null)
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

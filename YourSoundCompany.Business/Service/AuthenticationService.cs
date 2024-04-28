using YourSoundCompnay.Business.Model.User;
using YourSoundCompnay.Business.Model;
using YourSoundCompnay.RelationalData;
using C = BCrypt.Net;
using AutoMapper;
using System.Security.Claims;
using System.Text;
using YourSoundCompnay.RelationalData.Entities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace YourSoundCompnay.Business.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AuthenticationService
            (
                IUserRepository userRepository,
                IMapper mapper,
                IConfiguration configuration
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }


        public async Task<BaseResponse<UserLoginResponseModel>> Login(UserLoginModel model)
        {
            try
            {

                var result = new BaseResponse<UserLoginResponseModel>();
                var user = (await _userRepository.GetUserByEmail(model.Email)).FirstOrDefault();

                if (user is null)
                {
                    result.Message.Add("Email invalido!");
                    return result;
                }

                if (!C.BCrypt.Verify(model.Password, user.Password))
                {
                    result.Message.Add("As senha não coincidem!");
                    return result;

                }

                var token = GenerateToken(user);

                result.Data = new UserLoginResponseModel() { Token = token, user = _mapper.Map<UserResponseModel>(user) };


                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string GenerateToken(UserEntity user, IEnumerable<Claim>? claimsUserLogged = null)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTDescriptor:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            Claim[] claims;
            if (claimsUserLogged != null && claimsUserLogged.Any())
            {
                claims = new ClaimsIdentity(claimsUserLogged).Claims.ToArray();
            }
            else
            {
                claims = new[]
                {
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                };
            }
            var token = new JwtSecurityToken(_configuration["JWTDescriptor:Issuer"],
                _configuration["JWTDescriptor:Audience"],
                claims,
                expires: DateTime.Now.AddHours(6),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

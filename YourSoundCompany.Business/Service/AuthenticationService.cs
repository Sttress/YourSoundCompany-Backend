using YourSoundCompnay.Business.Model.User;
using YourSoundCompnay.Business.Model;
using C = BCrypt.Net;
using AutoMapper;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using YourSoundCompany.Business.Model.Authentication;
using System.Security.Cryptography;
using YourSoundCompany.CacheService.Service;
using YourSoundCompany.Business;

namespace YourSoundCompnay.Business.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ICacheService _cacheService;
        private readonly IUtilsService _utilsService;

        public AuthenticationService
            (
                IMapper mapper,
                IConfiguration configuration,
                IUserService userService,
                ICacheService cacheService,
                IUtilsService utilsService
            )
        {
            _mapper = mapper;
            _configuration = configuration;
            _userService = userService;
            _cacheService = cacheService;
            _utilsService = utilsService;
        }

        private string _Key_RefreshToken(string refreshToken) => $"key_token_{refreshToken}";

        public async Task<BaseResponse<UserLoginResponseModel>> Login(UserLoginModel model)
        {
            try
            {
                var result = new BaseResponse<UserLoginResponseModel>();
                result.Message = new List<string>();

                if (string.IsNullOrEmpty(model.Email))
                {
                    result.Message.Add("Email é obrigatório para o login!");
                    return result;
                }

                var usersByEmail = await _userService.GetByEmail(model.Email);

                var user = usersByEmail.Where(e => e.Active).FirstOrDefault();

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

                if (!user.Active)
                {
                    result.Message.Add("Você ainda não ativou a sua conta!");
                    return result;
                }

                var userModel = _mapper.Map<UserModel>(user);
                var auth = await GetAuth(user);

                result.Data = new UserLoginResponseModel() { Token = auth.Token,RefreshToken = auth.RefreshToken ,user = _mapper.Map<UserResponseModel>(user) };


                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<AuthModel> GetAuth(UserModel user)
        {
            var result = new AuthModel();

            result.Token = GenerateToken(user);
            result.RefreshToken = _utilsService.GenerateRandomString();
            await SaveRefreshToken(result.RefreshToken);
            return result;
        }
        private async Task RemoveRefreshTokenInCache(string refreshToken)
        {
            await _cacheService.Remove(_Key_RefreshToken(refreshToken));
        }

        private async Task SaveRefreshToken(string refreshToken)
        {
            await _cacheService.Set(_Key_RefreshToken(refreshToken), refreshToken, TimeSpan.FromHours(6));
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string Token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTDescriptor:SecretKey"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var pricipal = tokenHandler.ValidateToken(Token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
               !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid Token");

            return pricipal;
        }

        public async Task<BaseResponse<UserLoginResponseModel>> RefreshToken(AuthModel model)
        {

            try
            {
                var result = new BaseResponse<UserLoginResponseModel>();

                var principal = GetPrincipalFromExpiredToken(model.Token);
                var saveRefreshToken = await GetRefreshToken(model.RefreshToken);
                if (saveRefreshToken == null || saveRefreshToken != model.RefreshToken)
                {
                    result.Message.Add("RefreshToken invalido!");
                    return result;
                }

                if (!long.TryParse(principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.SerialNumber)?.Value, out long tenantId))
                {
                    result.Message.Add("RefreshToken invalido!");
                    return result;
                }

                var email = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
                if (string.IsNullOrEmpty(email))
                {
                    result.Message.Add("RefreshToken invalido!");
                    return result;
                }

                var usersByEmail = await _userService.GetByEmail(email);

                var user = usersByEmail.Where(e => e.Active is true).FirstOrDefault();

                if(user is null)
                {
                    result.Message.Add("Usuário não pode ser encontrado");
                }

                var newJwtToken = GenerateToken(user, principal.Claims);
                var newRefreshToken = _utilsService.GenerateRandomString();
                await SaveRefreshToken(newRefreshToken);

                result.Data = new UserLoginResponseModel() { Token = newJwtToken, RefreshToken = newRefreshToken, user = _mapper.Map<UserResponseModel>(user) };
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<string?> GetRefreshToken(string token)
        {
            return await _cacheService.Get<string>(_Key_RefreshToken(token));
        }

        private string GenerateToken(UserModel user, IEnumerable<Claim>? claimsUserLogged = null)
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

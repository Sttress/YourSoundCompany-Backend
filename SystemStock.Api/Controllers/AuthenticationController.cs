using Microsoft.AspNetCore.Mvc;
using SystemStock.Business.Model.User;
using SystemStock.Business.Model;
using SystemStock.Business.Service;
using Microsoft.AspNetCore.Authorization;

namespace SystemStock.Api.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService) 
        { 
            _authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<BaseResponse<UserLoginResponseModel>> Login([FromBody] UserLoginModel model)
        {
            return await _authenticationService.Login(model);
        }
    }
}

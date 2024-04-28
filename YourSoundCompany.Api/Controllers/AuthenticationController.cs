using Microsoft.AspNetCore.Mvc;
using YourSoundCompnay.Business.Model.User;
using YourSoundCompnay.Business.Model;
using Microsoft.AspNetCore.Authorization;
using YourSoundCompnay.Business;

namespace YourSoundCompnay.Api.Controllers
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

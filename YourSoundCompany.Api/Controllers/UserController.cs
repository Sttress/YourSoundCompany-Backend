using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using YourSoundCompnay.Business.Model;
using YourSoundCompnay.Business.Model.User;
using YourSoundCompnay.Business;

namespace YourSoundCompnay.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost("CreateUpdate")]
        [AllowAnonymous]
        public async Task<BaseResponse<UserResponseModel>> CreateUpdate([FromBody] UserCreateModel model)
        {
            return await _userService.CreateUpdate(model);
        }

        [HttpGet("")]
        public async Task<BaseResponse<UserResponseModel>> GetById([FromQuery] long id)
        {
            return await _userService.GetById(id);
        }

    }
}

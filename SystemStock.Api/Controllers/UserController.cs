using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SystemStock.Business.Model;
using SystemStock.Business.Model.User;
using SystemStock.Business.Service;

namespace SystemStock.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost("")]
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

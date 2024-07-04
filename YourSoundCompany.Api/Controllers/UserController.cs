﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using YourSoundCompnay.Business.Model;
using YourSoundCompnay.Business.Model.User;
using YourSoundCompnay.Business;
using YourSoundCompany.Business.Model.User;

namespace YourSoundCompnay.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost("Create")]
        [AllowAnonymous]
        public async Task<BaseResponse<UserResponseModel>> Create([FromBody] UserCreateModel model)
        {
            return await _userService.Create(model);
        }

        [HttpPost("Update")]
        public async Task<BaseResponse<UserResponseModel>> Update([FromBody] UserCreateModel model)
        {
            return await _userService.Update(model);
        }

        [HttpGet("")]
        public async Task<BaseResponse<UserResponseModel>> GetById([FromQuery] long id)
        {
            return await _userService.GetById(id);
        }

        //[HttpPut("RecoveryPassword")]
        //[AllowAnonymous]
        //public async Task<BaseResponse<UserResponseModel>> RecoveryPassword([FromQuery] string? email = "")
        //{
        //    return await _userService.RecoveryPassword(email);
        //}

        [HttpPost("VerifyEmailCode")]
        [AllowAnonymous]
        public async Task<BaseResponse<UserResponseModel>> VerifyEmailCode([FromBody] UserVerificationEmail model)
        {
            return await _userService.VerifyEmailCode(model);
        }
    }
}

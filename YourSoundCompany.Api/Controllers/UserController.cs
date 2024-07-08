using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using YourSoundCompnay.Business.Model;
using YourSoundCompnay.Business;
using YourSoundCompany.Business.Model.User.DTO;

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
        public async Task<BaseResponse<UserResponseDTO>> Create([FromBody] UserCreateDTO model)
        {
            return await _userService.Create(model);
        }

        [HttpPost("Update")]
        public async Task<BaseResponse<UserResponseDTO>> Update([FromBody] UserUpdateDTO model)
        {
            return await _userService.Update(model);
        }

        [HttpGet("")]
        public async Task<BaseResponse<UserResponseDTO>> GetById([FromQuery] long id)
        {
            return await _userService.GetById(id);
        }

        [HttpPost("RecoveryPassword")]
        [AllowAnonymous]
        public async Task<BaseResponse<UserResponseDTO>> RecoveryPassword([FromQuery] string email)
        {
            return await _userService.RecoveryPassword(email);
        }

        [HttpPost("RecoveryPasswordVerified")]
        [AllowAnonymous]
        public async Task<BaseResponse<UserResponseDTO>> RecoveryPasswordVerified([FromBody]UserRecoveryPasswordVerifiedDTO model )
        {
            return await _userService.RecoveryPasswordVerified(model);
        }

        [HttpPost("VerifyEmailCode")]
        [AllowAnonymous]
        public async Task<BaseResponse<UserResponseDTO>> VerifyEmailCode([FromBody] UserVerificationEmailDTO model)
        {
            return await _userService.VerifyEmailCode(model);
        }
    }
}

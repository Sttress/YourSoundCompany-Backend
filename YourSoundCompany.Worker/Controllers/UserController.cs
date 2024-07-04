using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sync;
using System.Security.AccessControl;
using YourSoundCompnay.Worker.Controllers;

namespace YourSoundCompany.Worker.Controllers
{

    public class UserController : BaseController
    {
        private readonly IUserSyncService _userSyncService;
        public UserController(IUserSyncService userSyncService) 
        {
            _userSyncService = userSyncService;
        }
        [HttpDelete("DeleteUserDrash")]
        public async Task DeleteUserDrash()
        {
            await _userSyncService.DeleteUserDrash();
        }
    }
}

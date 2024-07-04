using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourSoundCompany.Business;
using YourSoundCompany.Business.Model.Spotify;
using YourSoundCompnay.Business.Model;

namespace YourSoundCompnay.Api.Controllers
{
    public class SpotifyController : BaseController
    {
        private readonly ISpotifyService _spotifyService;
        public SpotifyController(ISpotifyService spotifyService) 
        {
            _spotifyService = spotifyService;
        }

        [HttpGet("GetUrlForCode")]
        public async Task<string> GetUrlForCode([FromQuery] string email)
        {
            return await _spotifyService.GetCodeUrl(email);
        }

        [HttpPost(("GetAuthorization"))]
        public async Task<OkResult> GetAuthorization([FromQuery] string code, string email)
        {
            await _spotifyService.GetAuthorization(email,code);
            return Ok();
        }
        [HttpPost("GetDashBoard")]
        public async Task<BaseResponse<DashBoardSpotifyModel>> GetDashBoard([FromBody] GetDashBoardSpotifyModel model)
        {
            return await _spotifyService.GetDashBoard(model);
        }
    }
}

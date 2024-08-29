using Microsoft.AspNetCore.Mvc;
using YourSoundCompany.Business;

namespace YourSoundCompnay.Api.Controllers
{
    public class SpotifyController : BaseController
    {
        private readonly ISpotifyService _spotifyService;
        public SpotifyController(ISpotifyService spotifyService) 
        {
            _spotifyService = spotifyService;
        }

        [HttpGet("GetUrlAuthorization")]
        public async Task<string> GetUrlAuthorization([FromQuery] string email)
        {
            return await _spotifyService.GetUrlAuthorization(email);
        }

        [HttpPost(("GetToken"))]
        public async Task<OkResult> GetAuthorization([FromQuery] string code, string email)
        {
            await _spotifyService.GetToken(email,code);
            return Ok();
        }

        //[HttpPost("GetDashBoard")]
        //public async Task<BaseResponse<DashBoardSpotifyModel>> GetDashBoard([FromBody] GetDashBoardSpotifyModel model)
        //{
        //    return await _spotifyService.GetDashBoard(model);
        //}
    }
}

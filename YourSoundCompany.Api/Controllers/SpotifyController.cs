using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("GetUrlForCode")]
        [AllowAnonymous]
        public async Task<string> GetUrlForCode()
        {
            return await _spotifyService.GetCodeUrl();
        }
    }
}

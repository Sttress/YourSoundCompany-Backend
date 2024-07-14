using Microsoft.Extensions.Configuration;
using YourSoundCompany.Business.Model.Spotify;
using YourSoundCompany.CacheService.Service;
using YourSoundCompany.IntegrationSpotify;
using YourSoundCompany.IntegrationSpotify.Model.User;
using YourSoundCompnay.Business;
using YourSoundCompnay.Business.Model;


namespace YourSoundCompany.Business.Service
{
    public class SpotifyService : ISpotifyService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cacheService;
        private readonly IUserService _userService;
        private readonly ISpotifyAuthService _spotifyAuthService;


        public SpotifyService(
            HttpClient httpClient,
            IConfiguration configuration,
            ICacheService cacheService,
            IUserService userService,
            ISpotifyAuthService spotifyAuthService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _cacheService = cacheService;
            _userService = userService;
            _spotifyAuthService = spotifyAuthService;
        }

        public async Task GetAuthorization(string email, string code)
        {
             await _spotifyAuthService.GetAuthorization(email,code);
        }

        public async Task<string> GetCodeUrl(string email)
        {
            return await _spotifyAuthService.GetCodeUrl(email);
        }

        public async Task<BaseResponse<DashBoardSpotifyModel>> GetDashBoard(GetDashBoardSpotifyModel model)
        {
            try
            {
                var result = new BaseResponse<DashBoardSpotifyModel>();
                var allArtists = new List<Artist>();
                var user = await _userService.GetCurrentUser();

                int limit = 50;
                int offset = 0;

                if (user is not object)
                {
                    result.Message.Add("Usuario logado não pode ser encontrato");
                    return result;
                }

                //var token = await GetTokenSpotify(user.Email);

                //if (string.IsNullOrEmpty(token))
                //{
                //    result.Message.Add("Usuario logado não pode ser encontrato");
                //    return result;
                //}


                var timeRangeTerm = GetTimeRangeTerm(model.TimeRange);
                         

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Fail in get dashboard", ex);
            }
        }

        private string GetTimeRangeTerm(string timeRange)
        {
            switch (timeRange)
            {
                case "short": return "short_term";
                case "medium":return "medium_term";
                case "long": return "long_term";
                default: return "short_term";
            }
        }
    }
}

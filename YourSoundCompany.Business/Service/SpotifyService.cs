using YourCompany.SpotifyService;
using YourSoundCompany.Business.Model.User.DTO;
using YourSoundCompany.CacheService.Service;
using YourSoundCompnay.Business.Model;
using YourSoundCompnay.SesseionService;


namespace YourSoundCompany.Business.Service
{
    public class SpotifyService : ISpotifyService
    {
        private readonly ISpotifyAuthService _spotifyAuthService;
        private readonly ICacheService _cacheService;
        private readonly ISessionService _sessionService;
        private readonly ISpotifyUserService _spotifyUserService;

        private string _Key_GetDashBoard(string email) => $"GetDashBoard{email}";


        public SpotifyService
            (
                ISpotifyAuthService spotifyAuthService,
                ICacheService cacheService,
                ISessionService sessionService,
                ISpotifyUserService spotifyUserService
            )
        {
            _spotifyAuthService = spotifyAuthService;
            _cacheService = cacheService;
            _sessionService = sessionService;
            _spotifyUserService = spotifyUserService;

        }

        public async Task GetToken(string email, string code)
        {
             await _spotifyAuthService.GetToken(email,code);
        }

        public async Task<string> GetUrlAuthorization(string email)
        {
            return await _spotifyAuthService.GetUrlAuthorization(email);
        }



        public async Task<BaseResponse<UserDashBoardDTO>> GetDashBoardCurrent()
        {
            try
            {
                var result = new BaseResponse<UserDashBoardDTO>();
                var email = _sessionService.Email;
                var key = _Key_GetDashBoard(email);

                var dashBoard = await _cacheService.Get<UserDashBoardDTO>(key);
                if(dashBoard is not null)
                {
                    result.Data = dashBoard;
                    return result;
                }

                dashBoard = await GetDashBoard();
                result.Data = dashBoard;
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<UserDashBoardDTO> GetDashBoard()
        {

            try
            {
                var artistList = await _spotifyUserService.GetTopArtistUserItems();
                var trackList = await _spotifyUserService.GetTopTracksUserItems();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

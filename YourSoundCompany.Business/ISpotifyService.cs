

using YourSoundCompany.Business.Model.Spotify;
using YourSoundCompnay.Business.Model;

namespace YourSoundCompany.Business
{
    public interface ISpotifyService
    {
        Task<string> GetCodeUrl(string email);
        Task GetAuthorization(string email, string code);
        Task<BaseResponse<DashBoardSpotifyModel>> GetDashBoard(GetDashBoardSpotifyModel model);
    }

}

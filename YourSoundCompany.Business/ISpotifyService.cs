

using YourSoundCompany.Business.Model.Spotify;
using YourSoundCompnay.Business.Model;

namespace YourSoundCompany.Business
{
    public interface ISpotifyService
    {
        Task<string> GetUrlAuthorization(string email);
        Task GetToken(string email, string code);
    }

}

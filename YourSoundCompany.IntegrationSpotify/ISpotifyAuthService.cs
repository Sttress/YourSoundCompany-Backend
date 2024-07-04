
namespace YourSoundCompany.IntegrationSpotify
{
    public interface ISpotifyAuthService
    {
        Task<string> GetCodeUrl(string email);
        Task GetAuthorization(string email, string code);
    }
}

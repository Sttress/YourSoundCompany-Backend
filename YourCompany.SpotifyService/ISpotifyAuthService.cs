using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourCompany.SpotifyService
{
    public interface ISpotifyAuthService
    {
        Task<string> GetUrlAuthorization(string email);
        Task GetToken(string email, string code);
    }
}

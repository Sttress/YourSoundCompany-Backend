using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourCompany.SpotifyService.Model;

namespace YourCompany.SpotifyService
{
    public interface ISpotifyUserService
    {
        Task<List<SpotifyArtistsModel>> GetTopArtistUserItems();
        Task<List<SpotifyTracksModel>> GetTopTracksUserItems();
    }
}

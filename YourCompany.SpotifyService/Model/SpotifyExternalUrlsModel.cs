using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourCompany.SpotifyService.Model
{
    public class SpotifyExternalUrlsModel
    {
        [JsonProperty("spotify")]
        public string Spotify { get; set; }
    }
}

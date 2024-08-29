using Newtonsoft.Json;

namespace YourCompany.SpotifyService.Model
{
    public class SpotifyFollowersModel
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}

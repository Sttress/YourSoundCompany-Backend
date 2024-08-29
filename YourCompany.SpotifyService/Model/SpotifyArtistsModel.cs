using Newtonsoft.Json;

namespace YourCompany.SpotifyService.Model
{
    public class SpotifyArtistsModel
    {
        [JsonProperty("external_urls")]
        public SpotifyExternalUrlsModel ExternalUrls { get; set; }

        [JsonProperty("followers")]
        public SpotifyFollowersModel Followers { get; set; }

        [JsonProperty("genres")]
        public List<string> Genres { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("images")]
        public List<SpotifyImageModel> Images { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("popularity")]
        public int Popularity { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}

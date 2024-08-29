using Newtonsoft.Json;

namespace YourCompany.SpotifyService.Model.Response
{
    public class UserTopItemsSpotifyResponse<T>
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("items")]
        public List<T> Items { get; set; }

    }

    
}

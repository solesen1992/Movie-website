using System.Text.Json.Serialization;

namespace Movie_website.Models
{
    public class Video
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("site")]
        public string Site { get; set; }
    }
}

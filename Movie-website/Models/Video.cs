using System.Text.Json.Serialization;

/*
 * Video
 * 
 * Represents a video associated with a movie or series, such as a trailer or behind-the-scenes video.
 * The `id`, `key`, `type`, and `site` properties are mapped directly from the `videos` JSON array in the TMDb API response.
 */

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

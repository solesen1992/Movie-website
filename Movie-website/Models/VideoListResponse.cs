using System.Text.Json.Serialization;

namespace Movie_website.Models
{
    public class VideoListResponse
    {
        [JsonPropertyName("results")]
        public List<Video> Results { get; set; }
    }
}

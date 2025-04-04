using Movie_website.Models;
using System.Text.Json.Serialization;

namespace Movie_website.ResponseModels
{
    public class VideoListResponse
    {
        [JsonPropertyName("results")]
        public List<Video> Results { get; set; }
    }
}

using Movie_website.Models;
using System.Text.Json.Serialization;

/*
 * VideoListResponse
 * 
 * This model represents the `results` array under the `videos` property in the response from The Movie Database API.
 * It contains a list of video objects (such as trailers or clips), each of which includes information like `id`, `key`, `type`, and `site`.
 * This class helps map video data to C# objects for easier handling.
 */

namespace Movie_website.ResponseModels
{
    public class VideoListResponse
    {
        [JsonPropertyName("results")]
        public List<Video> Results { get; set; }
    }
}

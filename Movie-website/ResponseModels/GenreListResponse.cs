using Movie_website.Models;
using System.Text.Json.Serialization;

namespace Movie_website.ResponseModels
{
    public class GenreListResponse
    {
        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace Movie_website.Models
{
    public class GenreListResponse
    {
        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; }
    }
}

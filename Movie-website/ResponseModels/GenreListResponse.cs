using Movie_website.Models;
using System.Text.Json.Serialization;

/*
 * GenreListResponse
 * 
 * This model represents the `genres` array returned by The Movie Database API.
 * It contains a list of genres, each of which has an `id` and a `name`. This helps map the genre data 
 * from the API response to a C# object for easier usage in the application.
 */

namespace Movie_website.ResponseModels
{
    public class GenreListResponse
    {
        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; }
    }
}

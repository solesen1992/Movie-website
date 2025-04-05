using System.Text.Json.Serialization;

/*
 * Genre
 * 
 * Represents a genre for movies or series. It includes the `id` and `name` properties that are mapped directly
 * from the `genres` JSON array returned by the TMDb API.
 */

namespace Movie_website.Models
{
    public class Genre
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}

using System.Text.Json.Serialization;

/*
 * Cast
 * 
 * Represents an actor in a movie or series, including their name and the character they play.
 * This class maps the `name` and `character` properties from the TMDb API response to a strongly-typed object.
 */

namespace Movie_website.Models
{
    public class Cast
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("character")]
        public string Character { get; set; }
    }
}

using System.Text.Json.Serialization;

/*
 * Crew
 * 
 * Represents a crew member involved in the production of a movie or series, such as a director, producer, or other key staff.
 * This class maps the `name` and `job` properties from the TMDb API response to a strongly-typed object.
 */

namespace Movie_website.Models
{
    public class Crew
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("job")]
        public string Job { get; set; }
    }
}

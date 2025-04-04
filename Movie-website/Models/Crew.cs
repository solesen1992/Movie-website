using System.Text.Json.Serialization;

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

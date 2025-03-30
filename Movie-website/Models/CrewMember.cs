using System.Text.Json.Serialization;

namespace Movie_website.Models
{
    public class CrewMember
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("job")]
        public string Job { get; set; }
    }
}

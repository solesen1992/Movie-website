using System.Text.Json.Serialization;

namespace Movie_website.Models
{
    public class Actor
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("character")]
        public string Character { get; set; }
    }
}

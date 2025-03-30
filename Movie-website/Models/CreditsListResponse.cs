using System.Text.Json.Serialization;

namespace Movie_website.Models
{
    public class CreditsListResponse
    {
        [JsonPropertyName("cast")]
        public List<CastMember> Cast { get; set; }

        [JsonPropertyName("crew")]
        public List<CrewMember> Crew { get; set; }
    }
}

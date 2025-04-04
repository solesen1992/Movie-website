using Movie_website.Models;
using System.Text.Json.Serialization;

namespace Movie_website.ResponseModels
{
    public class CreditsListResponse
    {
        [JsonPropertyName("cast")]
        public List<Actor> Cast { get; set; }

        [JsonPropertyName("crew")]
        public List<Crew> Crew { get; set; }
    }
}

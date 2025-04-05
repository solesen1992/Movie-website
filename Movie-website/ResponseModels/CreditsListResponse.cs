using Movie_website.Models;
using System.Text.Json.Serialization;

/*
 * CreditsListResponse
 * 
 * This model represents the `credits` information returned by The Movie Database API.
 * It contains two main lists: `cast` (actors and actresses) and `crew` (directors, producers, etc.).
 * This class maps the JSON response to a C# object, making it easier to work with movie and series credits data.
 */

namespace Movie_website.ResponseModels
{
    public class CreditsListResponse
    {
        [JsonPropertyName("cast")]
        public List<Cast> Cast { get; set; }

        [JsonPropertyName("crew")]
        public List<Crew> Crew { get; set; }
    }
}

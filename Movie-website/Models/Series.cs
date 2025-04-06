using Movie_website.ResponseModels;
using System.Text.Json.Serialization;

/*
 * Series
 * 
 * Represents a TV series with details such as the name, overview, first air date, poster path, and more. 
 * Includes a `Genres` property for a list of genres, `Credits` for the cast and crew, and `Videos` for trailers or other videos.
 * The properties are mapped directly from the JSON response of the TMDb API.
 */

namespace Movie_website.Models
{
    public class Series
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("first_air_date")]
        public string FirstAirDate { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        // Gets the URL for the cover photo
        public string PosterUrl
        {
            get
            {
                if (string.IsNullOrEmpty(PosterPath))
                {
                    return "/img/poster-not-available.jpg"; // No poster is available
                }
                else
                {
                    return "https://image.tmdb.org/t/p/w500" + PosterPath;
                }
            }
        }

        // Gets the release year - but only the first 4 signs
        public string ReleaseYear
        {
            get
            {
                if (!string.IsNullOrEmpty(FirstAirDate))
                {
                    return FirstAirDate.Substring(0, 4); // For example "2023-10-01" → "2023"
                }
                else
                {
                    return ""; // Unknown year
                }
            }
        }

        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; }

        [JsonPropertyName("credits")]
        public CreditsListResponse Credits { get; set; }

        [JsonPropertyName("videos")]
        public VideoListResponse Videos { get; set; }

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }

        // Gets the URL for the background photo
        public string BackdropUrl
        {
            get
            {
                if (string.IsNullOrEmpty(BackdropPath))
                {
                    return "/img/no-backdrop-available.jpg"; // If no background photo is available
                }
                else
                {
                    return "https://image.tmdb.org/t/p/original" + BackdropPath;
                }
            }
        }
    }
}

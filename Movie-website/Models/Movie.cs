using Movie_website.ResponseModels;
using System.Text.Json.Serialization;

/*
 * Movie
 * 
 * This model represents a single movie, including details like title, overview, release date,
 * poster path, genres, credits, and videos.
 * 
 * Each property is mapped directly from the JSON response returned by the The Moviedatabase API.
 * 
 * The [JsonPropertyName()] attribute is necessary here as well, because The Moviedatabase API returns
 * property names in snake_case (e.g., "release_date", "poster_path"), while C# follows PascalCase.
 * 
 * By using this attribute, we ensure that each JSON field is correctly mapped to the corresponding
 * C# property during deserialization.
 */

namespace Movie_website.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; }

        [JsonPropertyName("credits")]
        public Credits Credits { get; set; }

        [JsonPropertyName("videos")]
        public VideoListResponse Videos { get; set; }

        // Gets the URL for the cover photo of the movie
        public string PosterUrl
        {
            get
            {
                if (string.IsNullOrEmpty(PosterPath))
                {
                    return "/img/poster-not-available.jpg"; // No poster available
                }
                else
                {
                    // Otherwise combine the URL with the picture path
                    return "https://image.tmdb.org/t/p/w500" + PosterPath;
                }
            }
        }

        // Gets the release year of the movie (the first 4 signs)
        public string ReleaseYear
        {
            get
            {
                // If the release year is not empty or null
                if (!string.IsNullOrEmpty(ReleaseDate))
                {
                    // Returns the first 4 signs (For example "2023-10-01" → "2023")
                    return ReleaseDate.Substring(0, 4);
                }
                else
                {
                    // If the date is gone, return an empty string
                    return "";
                }
            }
        }

        // Gets the URL for the background photo
        public string BackdropUrl
        {
            get
            {
                if (string.IsNullOrEmpty(BackdropPath))
                {
                    return "/img/no-backdrop-available.jpg";
                }
                else
                {
                    return "https://image.tmdb.org/t/p/original" + BackdropPath;
                }
            }
        }
    }
}

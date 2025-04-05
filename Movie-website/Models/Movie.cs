using Movie_website.ResponseModels;
using System.Text.Json.Serialization;

/*
 * Movie
 * 
 * Represents a movie with details such as the title, overview, release date, poster path, and more. It includes 
 * a `Genres` property for a list of genres, `Credits` for the cast and crew, and `Videos` for trailers or other videos.
 * The properties are mapped directly from the JSON response of the TMDb API.
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

        public string PosterUrl => string.IsNullOrEmpty(PosterPath) ? "" : $"https://image.tmdb.org/t/p/w500{PosterPath}";
        public string BackdropUrl => string.IsNullOrEmpty(BackdropPath) ? "" : $"https://image.tmdb.org/t/p/original{BackdropPath}";
        public string ReleaseYear => !string.IsNullOrEmpty(ReleaseDate) ? ReleaseDate.Substring(0, 4) : "";
    }
}

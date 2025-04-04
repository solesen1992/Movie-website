using Movie_website.ResponseModels;
using System.Text.Json.Serialization;

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

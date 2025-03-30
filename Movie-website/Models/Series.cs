using System.Text.Json.Serialization;

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

        public string PosterUrl => string.IsNullOrEmpty(PosterPath) ? "" : $"https://image.tmdb.org/t/p/w500{PosterPath}";
        public string ReleaseYear => !string.IsNullOrEmpty(FirstAirDate) ? FirstAirDate[..4] : "";

        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; }

        [JsonPropertyName("credits")]
        public CreditsListResponse Credits { get; set; }

        [JsonPropertyName("videos")]
        public VideoListResponse Videos { get; set; }

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }

        public string BackdropUrl => string.IsNullOrEmpty(BackdropPath) ? "" : $"https://image.tmdb.org/t/p/original{BackdropPath}";
    }
}

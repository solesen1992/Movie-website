using System.Text.Json.Serialization;

/*
 * ApiListResponse<T>
 * 
 * This model represents the JSON structure returned by The Movie Database API when you request a list of movies or series.
 * 
 * It is a generic class, meaning it can be used for both movies and series (or anything else) because of <T>.
 * 
 * Example:
 * - When you fetch movies, it will be ApiListResponse<Movie>
 * - When you fetch series, it will be ApiListResponse<Series>
 * 
 * Why use [JsonPropertyName()]?
 * The API returns property names in **snake_case** (e.g. "total_results").
 * In C#, we normally use **PascalCase** (TotalResults).
 * So we use JsonPropertyName to tell the deserializer:
 * "When you see 'total_results' in JSON, map it to 'TotalResults' in C#."
 */

namespace Movie_website.ResponseModels
{
    public class ApiListResponse<T>
    {
        [JsonPropertyName("results")]
        public List<T> Results { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }
    }
}

using System.Text.Json.Serialization;

/*
 * ApiListResponse<T>
 * 
 * This model represents the top-level JSON structure returned by The Movie Database API when you request a list of movies or series.
 * 
 * It is a generic class, meaning it can be used for both movies and series (or any other objects) by using the type parameter <T>.
 * 
 * Example:
 * - When you fetch movies, it will be ApiListResponse<Movie>
 * - When you fetch series, it will be ApiListResponse<Series>
 * 
 * The reason for using [JsonPropertyName()] is to handle the difference between snake_case (e.g., "total_results") used by the API 
 * and PascalCase (e.g., TotalResults) used in C# properties. This ensures proper deserialization from the API response.
 */

namespace Movie_website.ResponseModels
{
    public class ApiListResponse<T>
    {
        // The "results" array from the API response.
        // Each item is deserialized into a T object (e.g., Movie or Series).
        [JsonPropertyName("results")]
        public List<T> Results { get; set; }

        // The current page number of the results.
        [JsonPropertyName("page")]
        public int Page { get; set; }

        // The total number of pages available for the request.
        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        // The total number of results available across all pages (not just the current page).
        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }
    }
}

using Movie_website.Models;
using Movie_website.ResponseModels;
using System.Net.Http;

/*
 * MovieService
 * 
 * This service is responsible for communicating with The Movie Database API to fetch movie data.
 * 
 * What it does:
 * - Fetches movies by genre (GetMoviesByGenreAsync)
 * - Fetches movie details (GetMovieDetailsAsync)
 * 
 * All methods are asynchronous (async) because they make HTTP requests to an external API, which may take time.
 */

namespace Movie_website.Service
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        /*
         * Constructor
         * 
         * Initializes the HttpClient and reads the API key and base URL from configuration.
         * The HttpClient is used for making HTTP requests to the API.
         */
        public MovieService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["TheMovieDatabase:ApiKey"];
            _baseUrl = configuration["TheMovieDatabase:BaseUrl"];
        }

        /*
         * GetMoviesByGenreAsync()
         * 
         * Fetches a list of movies for a specific genre from The Movie Database API.
         * It is async because it sends an HTTP request and waits for the API response.
         * If the API returns no data or an error occurs, it returns an empty list of movies.
         */
        public async Task<ApiListResponse<Movie>> GetMoviesByGenreAsync(int genreId, int page = 1)
        {
            try
            {
                string url = $"{_baseUrl}discover/movie?api_key={_apiKey}&with_genres={genreId}&page={page}";
                var response = await _httpClient.GetFromJsonAsync<ApiListResponse<Movie>>(url);

                // Debugging: log the response to see if there is data
                Console.WriteLine($"Response: {response}");

                if (response != null)
                {
                    return response;
                }
                else
                {
                    // Return empty list if no data
                    return new ApiListResponse<Movie>
                    {
                        Results = new List<Movie>(),
                        TotalResults = 0,
                        Page = 0,
                        TotalPages = 0
                    };
                }
            }
            catch (Exception ex)
            {
                // If something goes wrong, log the error and return empty list
                Console.WriteLine($"Error fetching movies: {ex.Message}");
                return new ApiListResponse<Movie>
                {
                    Results = new List<Movie>(),
                    TotalResults = 0,
                    Page = 0,
                    TotalPages = 0
                };
            }
        }


        /*
         * GetMovieDetailsAsync()
         * 
         * Fetches detailed information about a specific movie from The Movie Database API.
         * The details include credits (actors, directors) and videos (e.g., trailers).
         * It is async because it waits for the API response.
         * If an error occurs, it returns an empty Movie object.
         */
        public async Task<Movie> GetMovieDetailsAsync(int movieId)
        {
            try
            {
                var url = $"{_baseUrl}movie/{movieId}?api_key={_apiKey}&append_to_response=credits,videos";
                var movie = await _httpClient.GetFromJsonAsync<Movie>(url);

                if (movie != null)
                {
                    return movie;
                }
                else
                {
                    // Return empty movie if no data
                    return new Movie();
                }
            }
            catch (Exception ex)
            {
                // If something goes wrong, log the error and return empty movie
                Console.WriteLine($"Error fetching details for movie with id {movieId}: {ex.Message}");
                return new Movie();
            }
        }
    }
}
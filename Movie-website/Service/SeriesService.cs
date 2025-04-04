using Movie_website.Models;
using Movie_website.ResponseModels;
using System.Net.Http;

/*
 * SeriesService
 * 
 * This service is responsible for communicating with The Movie Database API to fetch series data.
 * 
 * What it does:
 * - Fetches series by genre (GetSeriesByGenreAsync)
 * - Fetches details for a specific series (GetSeriesDetailsAsync)
 * 
 * All methods are async because they make HTTP requests 
 * to an external API, which takes time.
 */

namespace Movie_website.Service
{
    public class SeriesService : ISeriesService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        /*
         * Constructor
         * 
         * Sets up the HttpClient and reads API information (API key and base URL) from configuration.
         */
        public SeriesService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["TheMovieDatabase:ApiKey"];
            _baseUrl = configuration["TheMovieDatabase:BaseUrl"];
        }

        /*
         * GetSeriesByGenreAsync()
         * 
         * This method fetches series from The Movie Database API by a specific genre ID.
         * It is async because it sends an HTTP request to the API and waits for the response.
         * If the API returns no data or there is an error, it returns an empty list of series.
         */
        public async Task<ApiListResponse<Series>> GetSeriesByGenreAsync(int genreId, int page = 1)
        {
            try
            {
                var url = $"{_baseUrl}discover/tv?api_key={_apiKey}&with_genres={genreId}&page={page}";
                var response = await _httpClient.GetFromJsonAsync<ApiListResponse<Series>>(url);

                if (response != null)
                {
                    return response;
                }
                else
                {
                    // Return empty list if no data
                    return new ApiListResponse<Series>
                    {
                        Results = new List<Series>(),
                        TotalResults = 0,
                        Page = 0,
                        TotalPages = 0
                    };
                }
            }
            catch (Exception ex)
            {
                // If something goes wrong, log the error and return empty list
                Console.WriteLine($"Fejl ved hentning af serier: {ex.Message}");
                return new ApiListResponse<Series>
                {
                    Results = new List<Series>(),
                    TotalResults = 0,
                    Page = 0,
                    TotalPages = 0
                };
            }
        }

        /*
         * GetSeriesDetailsAsync()
         * 
         * This method fetches detailed information about one specific series from The Movie Database API.
         * It is async because it sends an HTTP request to the API and waits for the response.
         * If no data is returned or an error happens, it returns an empty Series object.
         */
        public async Task<Series> GetSeriesDetailsAsync(int id)
        {
            try
            {
                var url = $"{_baseUrl}tv/{id}?api_key={_apiKey}&append_to_response=credits,videos";
                var series = await _httpClient.GetFromJsonAsync<Series>(url);

                if (series != null)
                {
                    return series;
                }
                else
                {
                    // Return empty series if no data
                    return new Series();
                }
            }
            catch (Exception ex)
            {
                // If something goes wrong, log the error and return empty series
                Console.WriteLine($"Fejl ved hentning af detaljer for serie med id {id}: {ex.Message}");
                return new Series();
            }
        }
    }
}

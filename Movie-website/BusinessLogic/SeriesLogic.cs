using Movie_website.Models;
using Movie_website.Service;
using Movie_website.ViewModels;

/*
 * SeriesLogic
 * 
 * This class contains the business logic for handling operations related to series.
 * It interacts with the service layer to fetch and process data for displaying series by genre.
 * 
 * What it does:
 * - Fetches series data for a specific genre (GetSeriesByGenreAsync method)
 * - Fetches details of a specific series (GetSeriesDetailsAsync method)
 * 
 * The logic is separated from the controller, keeping the application more maintainable.
 */

namespace Movie_website.BusinessLogic
{
    public class SeriesLogic : ISeriesLogic
    {
        private readonly ISeriesService _seriesService;

        /*
         * Constructor
         * 
         * Constructor that injects the service layer to fetch series data
         */
        public SeriesLogic(ISeriesService seriesService)
        {
            _seriesService = seriesService;
        }

        /*
         * GetSeriesByGenreAsync()
         * 
         * Fetches a list of series for a specific genre by calling the service layer.
         * The service layer communicates with the external API to fetch the raw data.
         * The logic layer processes and formats the data (e.g., limiting the number of results) before returning it.
         * 
         * The 'isIndexPage' flag determines how many series are shown (6 on the homepage, 20 on the genre page).
         * This helps to optimize the data presented depending on the context (homepage vs. genre page).
         * 
         * This method is asynchronous because it relies on the service layer's API calls, which are time-consuming.
         * 
         * @param genreId The genre ID for filtering series.
         * @param genreName The name of the genre.
         * @param page The page number for pagination.
         * @param isIndexPage A flag that indicates if the request is for the homepage (6 results) or the genre page (20 results).
         * @returns A view model that contains the series data along with metadata like total count.
         */
        public async Task<SeriesGenreViewModel> GetSeriesByGenreAsync(int genreId, string genreName, int page = 1, bool isIndexPage = true)
        {
            var apiResponse = await _seriesService.GetSeriesByGenreAsync(genreId, page);

            // Use the isIndexPage flag to decide how many movies to show
            // If we are on the homepage, show 6 series; otherwise, show 20 series
            int seriesLimit = isIndexPage ? 6 : 18;

            // The object that is being returned, which contains the data we want to display in the view
            return new SeriesGenreViewModel
            {
                Id = genreId,
                Name = genreName,
                Series = apiResponse.Results.Take(seriesLimit).ToList(), // 6 on homepage, 20 on genre page
                TotalCount = apiResponse.TotalResults // Total count of series
            };
        }

        /*
         * GetDesiredGenres()
         * 
         * Returns a predefined list of genre IDs and names. This method provides the list of genres that should
         * be displayed in the application, which vary depending on the category (movies vs. series).
         * 
         * This method is important for keeping the genres consistent across the application and can be 
         * updated if the list of genres becomes dynamic or fetched from an external source.
         * 
         * @returns A list of tuples containing genre ID and name for series.
         */
        public List<(int Id, string Name)> GetDesiredGenres()
        {
            // Manually specified genres you want to display for series
            return new List<(int Id, string Name)>
            {
                (35, "Comedy"),
                (80, "Crime"),
                (99, "Documentary"),
                (18, "Drama"),
                (10749, "Romance"),
            };
        }

        /*
         * GetSeriesDetailsAsync()
         * 
         * This method fetches detailed information about a specific series by calling the service layer.
         * This method is asynchronous due to external API calls, which are required to fetch series data.
         * 
         * @param id The unique identifier for the series.
         * @returns The series object with detailed information.
         */
        public async Task<Series> GetSeriesDetailsAsync(int id)
        {
            var seriesDetails = await _seriesService.GetSeriesDetailsAsync(id);
            return seriesDetails;
        }
    }
}

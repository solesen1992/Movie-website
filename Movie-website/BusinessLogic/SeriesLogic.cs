using Movie_website.Models;
using Movie_website.Service;

namespace Movie_website.BusinessLogic
{
    public class SeriesLogic : ISeriesLogic
    {
        private readonly ISeriesService _seriesService;

        public SeriesLogic(ISeriesService seriesService)
        {
            _seriesService = seriesService;
        }

        /*
         * GetSeriesForGenreAsync()
         * 
         * This method fetches series for a specific genre from the API and creates a SeriesGenreViewModel with the data.
         * It is async because it waits for the API to return data.
         * Returns null if there are no series in this genre.
         * 
         * Heler method for the Index method.
         */
        // This method gets series by genre and maps it to a ViewModel
        public async Task<SeriesGenreViewModel> GetSeriesByGenreAsync(int genreId, string genreName, int page = 1)
        {
            var apiResponse = await _seriesService.GetSeriesByGenreAsync(genreId, page);

            return new SeriesGenreViewModel
            {
                Id = genreId,
                Name = genreName, // You can pass the actual genre name here if needed
                Series = apiResponse.Results.Take(page == 1 ? 6 : 20).ToList(), // 6 on homepage, 20 on genre page
                TotalCount = apiResponse.TotalResults
            };
        }

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

        // This method fetches the details of a specific series
        public async Task<Series> GetSeriesDetailsAsync(int id)
        {
            var seriesDetails = await _seriesService.GetSeriesDetailsAsync(id);
            return seriesDetails;
        }
    }
}

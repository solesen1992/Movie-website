using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_website.Service;
using Movie_website.Extensions;
using Movie_website.Models;

/***************************************************************
 * SeriesController
 * 
 * This controller is responsible for handling everything related to displaying TV series in the application.
 * 
 * What it does:
 * - Shows all series in a specific genre (Genre method)
 * - Shows detailed information about one series (Details method)
 * 
 * Some methods are async because they need to fetch data from an external API, which takes time.
 * Other methods are not async when they don't need to wait for data from outside.
 ***************************************************************/

namespace Movie_website.Controllers
{
    public class SeriesController : Controller
    {
        private readonly ISeriesService _seriesService;

        /**
         * Constructor
         */
        public SeriesController(ISeriesService seriesService)
        {
            _seriesService = seriesService;
        }

        public async Task<IActionResult> Index()
        {
            // Get the list of manually desired genres (no API call needed)
            var desiredGenres = GetDesiredGenres();

            var seriesGenres = new List<SeriesGenreViewModel>();
            foreach (var genre in desiredGenres)
            {
                // Fetch series for each genre (use the SeriesService to fetch by genre)
                var seriesGenre = await GetSeriesForGenreAsync(genre.Id, genre.Name);
                if (seriesGenre != null)
                {
                    seriesGenres.Add(seriesGenre);
                }
            }

            // Return the view with the series genres
            return View(seriesGenres);
        }

        private List<(int Id, string Name)> GetDesiredGenres()
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
         * GetSeriesForGenreAsync()
         * 
         * This method fetches series for a specific genre from the API and creates a SeriesGenreViewModel with the data.
         * It is async because it waits for the API to return data.
         * Returns null if there are no series in this genre.
         * 
         * Heler method for the Index method.
         */
        private async Task<SeriesGenreViewModel?> GetSeriesForGenreAsync(int genreId, string genreName)
        {
            // Get series from API
            var seriesResponse = await _seriesService.GetSeriesByGenreAsync(genreId);

            // If there are series, create a ViewModel with the first 6 series
            if (seriesResponse.Results.Any())
            {
                return new SeriesGenreViewModel
                {
                    Id = genreId,
                    Name = genreName,
                    Series = seriesResponse.Results.Take(6).ToList(),
                    TotalCount = seriesResponse.TotalResults
                };
            }

            // Return null if there are no series
            return null;
        }

        /*
         * Genre(int id, string name, int page = 1)
         * 
         * This method shows all series that belong to a specific genre. It fetches the series from an external API using ISeriesService.
         * 
         * It is async because it needs to wait for the API to return data.
         */
        public async Task<IActionResult> Genre(int id, string name, int page = 1)
        {
            var result = await _seriesService.GetSeriesByGenreAsync(id, page);

            // Save information for the view (genre name, total results, page number, genre id)
            ViewBag.GenreName = name;
            ViewBag.TotalResults = result.TotalResults;
            ViewBag.Page = page;
            ViewBag.GenreId = id;

            // Show the list of series in the view
            return View(result.Results);
        }

        /*
         * Details(int id)
         * 
         * This method shows detailed information about one specific series. It fetches the details from an external API using ISeriesService.
         * 
         * It is async because it needs to wait for the API to return data.
         */
        public async Task<IActionResult> Details(int id)
        {
            var series = await _seriesService.GetSeriesDetailsAsync(id);
            return View("Details", series);
        }
    }
}

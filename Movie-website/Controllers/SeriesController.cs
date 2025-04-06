using Microsoft.AspNetCore.Mvc;
using Movie_website.BusinessLogic;
using Movie_website.ViewModels;

/*
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
 * 
 * The controller interacts with the SeriesLogic layer for the business logic associated with series.
 */

namespace Movie_website.Controllers
{
    public class SeriesController : Controller
    {
        private readonly ISeriesLogic _seriesLogic;


        /*
         * Constructor
         * 
         * This constructor sets up the SeriesLogic to interact with the business logic layer.
         */
        public SeriesController(ISeriesLogic seriesLogic)
        {
            _seriesLogic = seriesLogic;
        }

        /*
         * Index()
         * 
         * This method displays the homepage for series. It fetches series by genre.
         * The method is asynchronous because it fetches data from an external API.
         */
        public async Task<IActionResult> Index()
        {
            // Retrieve the desired genres for series via the SeriesLogic.
            var desiredGenres = _seriesLogic.GetDesiredGenres();

            var seriesGenres = new List<SeriesGenreViewModel>();
            foreach (var genre in desiredGenres)
            {
                // Fetch series for each genre using the business logic layer
                var seriesGenre = await _seriesLogic.GetSeriesByGenreAsync(genre.Id, genre.Name, page: 1, isIndexPage: true);
                if (seriesGenre != null)
                {
                    seriesGenres.Add(seriesGenre);
                }
            }

            // Return the view with the series genres
            return View(seriesGenres);
        }


        /*
         * Genre(int id, string name, int page = 1)
         * 
         * This method shows all series in a specific genre.
         * The method is asynchronous as it fetches the series data from an external API.
         * 
         * It returns series for the selected genre and handles pagination.
         */
        public async Task<IActionResult> Genre(int id, string name, int page = 1)
        {
            // Get series from the business logic layer
            var result = await _seriesLogic.GetSeriesByGenreAsync(id, name, page, isIndexPage: false);

            // Pass necessary information to the view (genre name, total results, page number, genre ID)
            ViewBag.GenreName = name;
            ViewBag.TotalResults = result.TotalCount;
            ViewBag.Page = page;
            ViewBag.GenreId = id;

            // Show the series in the view
            return View(result.Series);
        }

        /*
         * Details(int id)
         * 
         * This method displays detailed information about a specific series.
         * The method is asynchronous as it fetches data from the API.
         */
        public async Task<IActionResult> Details(int id)
        {
            var series = await _seriesLogic.GetSeriesDetailsAsync(id);
            return View(series);
        }
    }
}

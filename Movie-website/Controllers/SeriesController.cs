using Microsoft.AspNetCore.Mvc;
using Movie_website.BusinessLogic;
using Movie_website.ViewModels;

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
        private readonly ISeriesLogic _seriesLogic;


        /**
         * Constructor
         */
        public SeriesController(ISeriesLogic seriesLogic)
        {
            _seriesLogic = seriesLogic;
        }

        public async Task<IActionResult> Index()
        {
            // Get the list of manually desired genres from helper method
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
         * This method shows all series that belong to a specific genre. It fetches the series from an external API using ISeriesService.
         * 
         * It is async because it needs to wait for the API to return data.
         */
        public async Task<IActionResult> Genre(int id, string name, int page = 1)
        {
            // Get series from the business logic layer
            var result = await _seriesLogic.GetSeriesByGenreAsync(id, name, page, isIndexPage: false);

            // Save the information for the view
            ViewBag.GenreName = name;
            ViewBag.TotalResults = result.TotalCount;
            ViewBag.Page = page;
            ViewBag.GenreId = id;

            return View(result.Series);
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
            var series = await _seriesLogic.GetSeriesDetailsAsync(id);
            return View(series);
        }
    }
}

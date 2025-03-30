using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_website.Service;
using Movie_website.Extensions;

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

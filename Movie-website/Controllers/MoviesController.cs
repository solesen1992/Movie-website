using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_website.Models;
using Movie_website.Service;
using Movie_website.Extensions;

/*
 * MoviesController
 * 
 * This controller is responsible for handling everything related to displaying movies in the application.
 * 
 * What it does:
 * - Shows all movies in a specific genre (Genre method)
 * - Shows detailed information about one movie (Details method)
 * 
 * Some methods are async because they need to fetch data from an external API, which takes time.
 */

namespace Movie_website.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        /**
         * Constructor
         */
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /*
         * Genre(int id, string name, int page = 1)
         * 
         * This method shows all movies that belong to a specific genre. It fetches the movies from the API using IMovieService.
         * It is async because it needs to wait for the API to return data.
         * 
         *  * The 'page = 1' means that if no page number is given in the URL, the method will automatically use page 1.
         */
        public async Task<IActionResult> Genre(int id, string name, int page = 1)
        {
            var result = await _movieService.GetMoviesByGenreAsync(id, page);

            // Save information for the view (genre name, total results, page number, genre id)
            ViewBag.GenreName = name;
            ViewBag.TotalResults = result.TotalResults;
            ViewBag.Page = page;
            ViewBag.GenreId = id;

            // Show the list of movies in the view
            return View(result.Results);
        }

        /*
         * Details(int id)
         * GET: MoviesController/Details/5
         * 
         * This method shows detailed information about one specific movie. It fetches the details from the API using IMovieService.
         * It is async because it needs to wait for the API to return data.
         * 
         * It also checks if the movie is in the user's wishlist by reading the wishlist from Session.
         */
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieDetailsAsync(id);

            // Try to get the wishlist from the session
            List<int> wishlist = HttpContext.Session.Get<List<int>>("wishlist");

            // If there is no wishlist yet, create an empty one
            if (wishlist == null)
            {
                wishlist = new List<int>();
            }

            // Check if the movie is in the wishlist and pass this info to the view
            ViewBag.IsInWishlist = wishlist.Contains(movie.Id);

            return View(movie);
        }
    }
}

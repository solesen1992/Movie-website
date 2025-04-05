using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_website.Service;
using Movie_website.Extensions;
using Movie_website.BusinessLogic;
using Movie_website.ViewModels;
using Movie_website.Models;

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
 * 
 * The controller interacts with the MovieLogic layer for the business logic associated with movies.
 */

namespace Movie_website.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieLogic _movieLogic;

        /*
         * Constructor
         * 
         * This constructor sets up the MovieLogic to interact with the business logic layer.
         */
        public MoviesController(IMovieLogic movieLogic)
        {
            _movieLogic = movieLogic;
        }

        /*
         * Index()
         * 
         * This method displays the homepage for movies. It fetches movies by genre 
         * and shows 6 movies per genre on the homepage.
         * 
         * The method is asynchronous because it fetches data from an external API.
         */
        public async Task<IActionResult> Index()
        {
            // Retrieve the desired genres for movies via the MovieLogic.
            var desiredGenres = _movieLogic.GetDesiredGenres();

            var movieGenres = new List<MovieGenreViewModel>();
            foreach (var genre in desiredGenres)
            {
                // Fetch movies for each genre
                var movieGenre = await _movieLogic.GetMoviesByGenreAsync(genre.Id, genre.Name, page: 1, isIndexPage: true);
                if (movieGenre != null)
                {
                    movieGenres.Add(movieGenre);
                }
            }

            // Return the view with the movie genres
            return View(movieGenres);
        }

        /*
         * Genre(int id, string name, int page = 1)
         * 
         * This method shows all movies in a specific genre.
         * The method is asynchronous as it fetches the movie data from an external API.
         * 
         * It returns movies for the selected genre and handles pagination.
         */
        public async Task<IActionResult> Genre(int id, string name, int page = 1)
        {
            // Fetch movies for the selected genre and page
            var result = await _movieLogic.GetMoviesByGenreAsync(id, name, page, isIndexPage: false);

            // Pass necessary information to the view (genre name, total results, page number, genre ID)
            ViewBag.GenreName = name;
            ViewBag.TotalResults = result.TotalCount;
            ViewBag.Page = page;
            ViewBag.GenreId = id;

            // Show the movies in the view
            return View(result.Movies);
        }


        /*
         * Details(int id)
         * GET: MoviesController/Details/5
         * 
         * This method shows detailed information about one specific movie.
         * The method is asynchronous as it fetches the movie details from the API.
         * 
         * It also checks if the movie is in the user's wishlist.
         */
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieLogic.GetMovieDetailsAsync(id);

            // Try to get the wishlist from the session
            List<int> wishlist = HttpContext.Session.Get<List<int>>("wishlist");

            // If no wishlist exists, create an empty list
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

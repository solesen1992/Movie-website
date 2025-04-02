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

        public async Task<IActionResult> Index()
        {
            // Get the list of manually desired genres (no API call needed)
            var desiredGenres = GetDesiredGenres();

            var movieGenres = new List<MovieGenreViewModel>();
            foreach (var genre in desiredGenres)
            {
                // Fetch movies for each genre (use the MovieService to fetch by genre)
                var movieGenre = await GetMoviesForGenreAsync(genre.Id, genre.Name);
                if (movieGenre != null)
                {
                    movieGenres.Add(movieGenre);
                }
            }

            // Return the view with the movie genres
            return View(movieGenres);
        }

        private List<(int Id, string Name)> GetDesiredGenres()
        {
            // Manually specified genres you want to display
            return new List<(int Id, string Name)>
            {
                (28, "Action"),
                (35, "Comedy"),
                (80, "Crime"),
                (99, "Documentary"),
                (18, "Drama"),
                (27, "Horror"),
                (10749, "Romance"),
                (53, "Thriller"),
                (10752, "War")
            };
        }

        /*
         * GetMoviesForGenreAsync()
         * 
         * This method fetches movies for a specific genre from the API and creates a MovieGenreViewModel with the data.
         * It is async because it waits for the API to return data.
         * Returns null if there are no movies in this genre.
         * 
         * Helper method for the Index method.
         */
        private async Task<MovieGenreViewModel?> GetMoviesForGenreAsync(int genreId, string genreName)
        {
            // Get movies from API
            var movieResponse = await _movieService.GetMoviesByGenreAsync(genreId);

            // If there are movies, create a ViewModel with the first 6 movies
            if (movieResponse.Results.Any())
            {
                return new MovieGenreViewModel
                {
                    Id = genreId,
                    Name = genreName,
                    Movies = movieResponse.Results.Take(6).ToList(),
                    TotalCount = movieResponse.TotalResults
                };
            }

            // Return null if there are no movies
            return null;
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

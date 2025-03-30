using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_website.Extensions;
using Movie_website.Models;
using Movie_website.Service;

/*
 * WishlistController
 * 
 * This controller is responsible for handling everything related to the user's wishlist.
 * 
 * What it does:
 * - Shows the user's wishlist (Index method)
 * - Allows the user to add a movie to the wishlist (Add method)
 * - Allows the user to remove a movie from the wishlist and go back to the movie details page (Remove method)
 * - Allows the user to remove a movie from the wishlist and stay on the wishlist page (RemoveAndReturn method)
 * 
 * How it works:
 * The wishlist is stored in the user's Session. Because Session can only store strings, 
 * the wishlist (which is a list of movie IDs) is saved as a JSON string using the SessionExtensions helper class.
 * 
 * Whenever the user adds or removes a movie, the wishlist is updated in the session.
 * 
 * The controller also uses IMovieService to fetch movie details so the wishlist page can show the actual movie information.
 * 
 * Some methods in this controller are async and some are not:
 * - Methods that need to fetch data from an external source (API) are async.
 * - Methods that only work with Session and do not wait for external data are NOT async.
 */

namespace Movie_website.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IMovieService _movieService;

        /**
         * Constructor
         */
        public WishlistController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /*
         * This method shows the user's wishlist. It gets the wishlist from the session (a list of movie IDs).
         * Then it loops through the IDs and fetches the movie details using IMovieService, so the view can show the movies.
         * 
         * It is marked as async because it needs to fetch movie details from an external API using IMovieService. 
         * Fetching data from an API takes time, so we use async/await to avoid blocking the server while waiting.
         */
        public async Task<IActionResult> Index()
        {
            // Try to get the wishlist from session
            List<int> wishlist = HttpContext.Session.Get<List<int>>("wishlist");

            // If nothing is saved in the session, make a new empty wishlist
            if (wishlist == null)
            {
                wishlist = new List<int>();
            }

            // Used to save the movies that's collected on the wishlist
            var movies = new List<Movie>();

            // Go through the wishlist and get the movies
            foreach (var id in wishlist)
            {
                var movie = await _movieService.GetMovieDetailsAsync(id);
                movies.Add(movie);
            }

            return View(movies);
        }

        /*
         * Add(int id)
         * 
         * This method adds a movie to the wishlist. It gets the wishlist from the session.
         * If the movie is not already in the wishlist, it adds the ID and saves the updated list back to the session.
         * After adding, it redirects the user back to the movie details page.
         * 
         * It is NOT async because it only works with Session and local data. It does not need to fetch data
         * from the API. It simply updates the wishlist in memory/the session and redirects.
         */
        [HttpPost]
        public IActionResult Add(int id)
        {
            List<int> wishlist = HttpContext.Session.Get<List<int>>("wishlist");

            if (wishlist == null)
            {
                wishlist = new List<int>();
            }

            // If the movie is not on the wishlist - then it should add it.
            if (!wishlist.Contains(id))
            {
                wishlist.Add(id);
                HttpContext.Session.Set("wishlist", wishlist);
            }

            // Create a route parameter object with the id
            var routeValues = new { id = id };

            // Redirect to the Details action in the Movies controller
            return RedirectToAction(actionName: "Details", controllerName: "Movies", routeValues);
        }

        /*
         * Remove(int id)
         * 
         * This method removes a movie from the wishlist. It gets the wishlist from the session.
         * If the movie ID exists in the list, it removes it. Then it saves the updated list back to the session.
         * After removing, it redirects the user back to the movie details page.
         * 
         * It is NOT async because it only works with Session and does not need to fetch data from the API.
         */
        [HttpPost]
        public IActionResult Remove(int id)
        {
            // Try to get the wishlist from the session
            List<int> wishlist = HttpContext.Session.Get<List<int>>("wishlist");

            // If there is no wishlist saved in the session yet, create a new empty list
            if (wishlist == null)
            {
                wishlist = new List<int>();
            }

            // Remove the movie with the given id from the wishlist
            wishlist.Remove(id);

            // Save the updated wishlist back into the session
            HttpContext.Session.Set("wishlist", wishlist);

            // Redirect the user back to the Details page of the movie they just removed
            return RedirectToAction(
                actionName: "Details",          // Action name
                controllerName: "Movies",       // Controller name
                routeValues: new { id = id }    // Route values (passing the id)
            );
        }

        /*
         * RemoveAndReturn(int id)
         * 
         * This method removes a movie from the wishlist. It works the same way as Remove(), but instead of redirecting 
         * to the movie details page, it redirects the user back to the wishlist page (Index).
         * 
         * It is not async because it only works with session data.
         */
        [HttpPost]
        public IActionResult RemoveAndReturn(int id)
        {
            // Try to get the wishlist from the session
            List<int> wishlist = HttpContext.Session.Get<List<int>>("wishlist");

            // If there is no wishlist yet, create an empty one
            if (wishlist == null)
            {
                wishlist = new List<int>();
            }

            // Remove the movie with the given id from the wishlist
            wishlist.Remove(id);

            // Save the updated wishlist back into the session
            HttpContext.Session.Set("wishlist", wishlist);

            // Redirect the user back to the wishlist page (Index action)
            return RedirectToAction(
                actionName: "Index" // Go back to the Index action (wishlist page)
            );
        }
    }
}

﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_website.Models;
using Movie_website.Service;
using Movie_website.Extensions;
using Movie_website.BusinessLogic;

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
        private readonly IMovieLogic _movieLogic;

        /**
         * Constructor
         */
        public MoviesController(IMovieLogic movieLogic)
        {
            _movieLogic = movieLogic;
        }

        public async Task<IActionResult> Index()
        {
            // Get the list of manually desired genres (no API call needed)
            var desiredGenres = _movieLogic.GetDesiredGenres();

            var movieGenres = new List<MovieGenreViewModel>();
            foreach (var genre in desiredGenres)
            {
                // Fetch movies for each genre (use the MovieLogic to fetch by genre)
                var movieGenre = await _movieLogic.GetMoviesByGenreAsync(genre.Id, genre.Name, page: 1, isIndexPage: true);
                if (movieGenre != null)
                {
                    movieGenres.Add(movieGenre);
                }
            }

            // Return the view with the movie genres
            return View(movieGenres);
        }

        /*private List<(int Id, string Name)> GetDesiredGenres()
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
        }*/

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
            var result = await _movieLogic.GetMoviesByGenreAsync(id, name, page, isIndexPage: false);

            // Save information for the view (genre name, total results, page number, genre id)
            ViewBag.GenreName = name;
            ViewBag.TotalResults = result.TotalCount;
            ViewBag.Page = page;
            ViewBag.GenreId = id;

            // Show the list of movies in the view
            return View(result.Movies);
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
            var movie = await _movieLogic.GetMovieDetailsAsync(id);

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

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Movie_website.Models;
using Movie_website.Service;

/*
 * HomeController
 * 
 * This controller is responsible for showing the homepage and general pages like Privacy and Error.
 * 
 * What it does:
 * - Shows the homepage with movies and series grouped by genre (Index method)
 * - Shows the privacy page (Privacy method)
 * - Shows the error page if something goes wrong (Error method)
 * 
 * Some methods are async because they need to fetch data from the API.
 * Other methods are not async when they just return a view without needing to fetch data.
 */

namespace Movie_website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ISeriesService _seriesService;

        /*
         * Constructor
         * 
         * This constructor initializes the services needed to get movies and series data.
         */
        public HomeController(IMovieService movieService, ISeriesService seriesService)
        {
            _movieService = movieService;
            _seriesService = seriesService;
        }

        /*
         * Index()
         * 
         * This method shows the homepage. It fetches movies and series by specific genres 
         * and displays a preview of 6 movies/series per genre.
         * The method has a lot of helper methods to prevent the method from being too long.
         * 
         * It is async because it needs to wait for the movie and series data from the API.
         */
        public async Task<IActionResult> Index()
        {
            // Get the list of genres we want to show on the homepage.
            var desiredGenres = GetDesiredGenres(); // Helper method

            // Lists to store movie and series genres with their data
            var movieGenres = new List<MovieGenreViewModel>();
            var seriesGenres = new List<SeriesGenreViewModel>();

            // Go through each genre and fetch movies and series
            foreach (var genre in desiredGenres)
            {
                // Get movies for this genre
                var movieGenre = await GetMoviesForGenreAsync(genre.Id, genre.Name); // Helper method
                if (movieGenre != null)
                {
                    movieGenres.Add(movieGenre);
                }

                // Get series for this genre
                var seriesGenre = await GetSeriesForGenreAsync(genre.Id, genre.Name); // Helper method
                if (seriesGenre != null)
                {
                    seriesGenres.Add(seriesGenre);
                }
            }

            // Create a view model with the lists of movies and series
            var homepageViewModel = new HomePageViewModel
            {
                MovieGenres = movieGenres,
                SeriesGenres = seriesGenres
            };

            // Send the view model to the view
            return View(homepageViewModel);
        }

        /*
         * GetDesiredGenres()
         * 
         * Returns a list of genre IDs and names that we want to display on the homepage.
         * Helper method for the Index method.
         */
        private List<(int Id, string Name)> GetDesiredGenres()
        {
            return new List<(int Id, string Name)>
            {
                (28, "Action"),
                (35, "Comedy"),
                (53, "Thriller"),
                (10752, "War"),
                (10749, "Romance"),
                (18, "Drama"),
                (80, "Crime"),
                (99, "Documentary"),
                (27, "Horror")
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


        /**************************************************************************
         * The following methods are autogenerated
         **************************************************************************/

        /*
         * Privacy()
         * 
         * This method shows the Privacy page.
         * It is not async because it does not need to fetch data.
         */
        public IActionResult Privacy()
        {
            return View();
        }

        /*
         * Error()
         * 
         * This method shows the Error page if something goes wrong.
         * It creates an ErrorViewModel that contains a RequestId to help identify the error.
         * 
         * It is not async because it only creates an object and returns a view — no need to wait for external data.
         */
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

            // Create a new ErrorViewModel object
            ErrorViewModel errorModel = new ErrorViewModel();

            // Check if Activity.Current is not null
            if (Activity.Current != null)
            {
                // Set the RequestId to the current Activity Id
                errorModel.RequestId = Activity.Current.Id;
            }
            else
            {
                // If Activity.Current is null, use the HttpContext TraceIdentifier
                errorModel.RequestId = HttpContext.TraceIdentifier;
            }

            // Pass the model to the View
            return View(errorModel);
        }
    }
}

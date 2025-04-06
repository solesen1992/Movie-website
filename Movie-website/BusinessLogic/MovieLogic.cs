using Movie_website.Models;
using Movie_website.Service;
using Movie_website.ViewModels;

/*
 * MovieLogic
 * 
 * This class is responsible for the business logic related to movies.
 * It acts as the intermediary between the controller and the service layer.
 * 
 * Why we have it:
 * - It separates the data fetching logic from the controller, ensuring that the controller remains lean and focused on managing HTTP requests.
 * - It also formats the data (e.g., by limiting the number of movies) and provides it in a structure that is easier to work with in the view layer.
 * 
 * What it does:
 * - Fetches movie data by genre and formats it into a view model (GetMoviesByGenreAsync method).
 * - Fetches movie details for a specific movie (GetMovieDetailsAsync method).
 * - Provides a list of genres that the application should display (GetDesiredGenres method).
 */

namespace Movie_website.BusinessLogic
{
    public class MovieLogic : IMovieLogic
    {
        private readonly IMovieService _movieService;

        /*
         * Constructor
         * 
         * Initializes the MovieLogic class with a dependency to the IMovieService.
         */
        public MovieLogic(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /*
         * GetMoviesByGenreAsync()
         * 
         * This method fetches movies for a specific genre by calling the service layer (IMovieService).
         * It processes the raw data and maps it to a MovieGenreViewModel before returning it.
         * 
         * Parameters:
         * - genreId: The ID of the genre for which we want to fetch movies.
         * - genreName: The name of the genre (used for display purposes).
         * - page: The page number for pagination (defaults to 1).
         * - isIndexPage: A flag indicating whether this request is for the homepage or the genre page (defaults to true).
         * 
         * Returns:
         * - A MovieGenreViewModel that contains the list of movies, genre name, and total number of results.
         * 
         * Why async?
         * - This method is asynchronous because it makes an external API call, which takes time. 
         * The async/await pattern allows the app to stay responsive while the data is being fetched.
         */
        public async Task<MovieGenreViewModel> GetMoviesByGenreAsync(int genreId, string genreName, int page = 1, bool isIndexPage = true)
        {
            var apiResponse = await _movieService.GetMoviesByGenreAsync(genreId, page);

            // Use the isIndexPage flag to decide how many movies to show
            int movieLimit = isIndexPage ? 6 : 20;

            // Return a MovieGenreViewModel with the data
            return new MovieGenreViewModel
            {
                Id = genreId,
                Name = genreName, // You can map or pass the genre name as required
                Movies = apiResponse.Results.Take(movieLimit).ToList(), // 6 on homepage, 20 on genre page
                TotalCount = apiResponse.TotalResults
            };
        }

        /*
         * GetDesiredGenres()
         * 
         * This method returns a predefined list of genre IDs and names.
         * These genres are displayed in the application and are predefined since not all genres need to be shown.
         * 
         * Returns:
         * - A list of tuples, each containing a genre ID and its name.
         */
        public List<(int Id, string Name)> GetDesiredGenres()
        {
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
         * GetMovieDetailsAsync()
         * 
         * This method fetches detailed information about a specific movie by calling the service layer.
         * It retrieves the detailed data and returns it to the controller for further processing or display.
         * 
         * Parameters:
         * - movieId: The ID of the movie for which we want to fetch detailed information.
         * 
         * Returns:
         * - A Movie object containing detailed information about the movie.
         * 
         * Why async?
         * - This method is asynchronous because it makes an external API call to fetch movie details, and the async/await pattern allows the app to remain responsive.
         */
        public async Task<Movie> GetMovieDetailsAsync(int movieId)
        {
            var movie = await _movieService.GetMovieDetailsAsync(movieId);
            return movie;
        }
    }
}

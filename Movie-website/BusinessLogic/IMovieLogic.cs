using Movie_website.Models;
using Movie_website.ViewModels;

/**
 * IMovieLogic
 * 
 * This interface defines the methods for business logic related to movies.
 * It serves as the contract for the MovieLogic class and provides methods to fetch movie data by genre,
 * fetch movie details, and retrieve a predefined list of genres for display purposes.
 * 
 * Why do we have it?
 * - It separates the controller logic from business logic, keeping controllers lightweight.
 * - It defines the structure for movie-related business logic which is implemented by the MovieLogic class.
 */

namespace Movie_website.BusinessLogic
{
    public interface IMovieLogic
    {
        Task<MovieGenreViewModel> GetMoviesByGenreAsync(int genreId, string genreName, int page = 1, bool isIndexPage = true);

        List<(int Id, string Name)> GetDesiredGenres();
        Task<Movie> GetMovieDetailsAsync(int movieId);
    }
}

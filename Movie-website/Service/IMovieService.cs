using Movie_website.Models;
using Movie_website.ResponseModels;

/*
 * IMovieService
 * 
 * This interface defines the methods that any MovieService class must implement.
 * It is used to fetch movie data from the API.
 */

namespace Movie_website.Service
{
    public interface IMovieService
    {
        Task<ApiListResponse<Movie>> GetMoviesByGenreAsync(int genreId, int page = 1);
        Task<Movie> GetMovieDetailsAsync(int movieId);
    }
}

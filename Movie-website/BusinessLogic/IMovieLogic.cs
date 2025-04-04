using Movie_website.Models;

namespace Movie_website.BusinessLogic
{
    public interface IMovieLogic
    {
        Task<MovieGenreViewModel> GetMoviesByGenreAsync(int genreId, string genreName, int page = 1, bool isIndexPage = true);

        List<(int Id, string Name)> GetDesiredGenres();
        Task<Movie> GetMovieDetailsAsync(int movieId);
    }
}

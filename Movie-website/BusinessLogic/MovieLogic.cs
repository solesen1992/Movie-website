using Movie_website.Models;
using Movie_website.Service;
using Movie_website.ViewModels;

namespace Movie_website.BusinessLogic
{
    public class MovieLogic : IMovieLogic
    {
        private readonly IMovieService _movieService;

        public MovieLogic(IMovieService movieService)
        {
            _movieService = movieService;
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
        public async Task<MovieGenreViewModel> GetMoviesByGenreAsync(int genreId, string genreName, int page = 1, bool isIndexPage = true)
        {
            var apiResponse = await _movieService.GetMoviesByGenreAsync(genreId, page);

            // Use the isIndexPage flag to decide how many movies to show
            int movieLimit = isIndexPage ? 6 : 20;

            return new MovieGenreViewModel
            {
                Id = genreId,
                Name = genreName, // You can map or pass the genre name as required
                Movies = apiResponse.Results.Take(movieLimit).ToList(), // 6 on homepage, 20 on genre page
                TotalCount = apiResponse.TotalResults
            };
        }

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

        public async Task<Movie> GetMovieDetailsAsync(int movieId)
        {
            var movie = await _movieService.GetMovieDetailsAsync(movieId);
            return movie;
        }
    }
}

using Movie_website.ViewModels;

namespace Movie_website.BusinessLogic
{
    public class HomeLogic : IHomeLogic
    {
        private readonly IMovieLogic _movieLogic;
        private readonly ISeriesLogic _seriesLogic;

        public HomeLogic(IMovieLogic movieLogic, ISeriesLogic seriesLogic)
        {
            _movieLogic = movieLogic;
            _seriesLogic = seriesLogic;
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
        public async Task<HomePageViewModel> GetHomePageDataAsync()
        {
            var desiredGenres = GetDesiredGenres(); // Helper method to return genre data

            var homepageViewModel = new HomePageViewModel
            {
                MovieGenres = new List<MovieGenreViewModel>(),
                SeriesGenres = new List<SeriesGenreViewModel>()
            };

            // Fetch movie and series data for each genre
            foreach (var genre in desiredGenres)
            {
                var movieGenre = await _movieLogic.GetMoviesByGenreAsync(genre.Id, genre.Name);
                if (movieGenre != null)
                {
                    homepageViewModel.MovieGenres.Add(movieGenre);
                }

                var seriesGenre = await _seriesLogic.GetSeriesByGenreAsync(genre.Id, genre.Name);
                if (seriesGenre != null)
                {
                    homepageViewModel.SeriesGenres.Add(seriesGenre);
                }
            }

            return homepageViewModel;
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
    }
}

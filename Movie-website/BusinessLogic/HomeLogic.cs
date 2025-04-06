using Movie_website.ViewModels;

/* 
 * HomeLogic
 * 
 * This class handles the business logic for the homepage.
 * It retrieves data for both movies and series by their genres, processes it, and provides a unified view model
 * containing the necessary data for the homepage view.
 * 
 * Why do we have it?
 * - It acts as the bridge between the controller and the service layer. It fetches and formats data before passing it to the controller.
 * - It centralizes the logic of retrieving movies and series data for the homepage.
 */

namespace Movie_website.BusinessLogic
{
    public class HomeLogic : IHomeLogic
    {
        private readonly IMovieLogic _movieLogic;
        private readonly ISeriesLogic _seriesLogic;

        /*
         * Constructor
         * 
         * Initializes the HomeLogic class with dependencies for the movie and series business logic.
         * 
         * Parameters:
         * - movieLogic: Provides methods to fetch and process movie-related data.
         * - seriesLogic: Provides methods to fetch and process series-related data.
         */
        public HomeLogic(IMovieLogic movieLogic, ISeriesLogic seriesLogic)
        {
            _movieLogic = movieLogic;
            _seriesLogic = seriesLogic;
        }

        /* 
         * GetHomePageDataAsync()
         * 
         * This method fetches the necessary data for the homepage.
         * It retrieves movie and series data by genre using the movie and series logic layers, processes it, and
         * packages it into a HomePageViewModel.
         * 
         * Why async?
         * - The method is asynchronous because it involves calling external data sources (API), which may take time.
         * 
         * Returns:
         * - A populated HomePageViewModel containing movies and series data for display on the homepage.
         */
        public async Task<HomePageViewModel> GetHomePageDataAsync()
        {
            // Fetch the genres for movies and series separately from each business logic class
            var movieGenres = _movieLogic.GetDesiredGenres();
            var seriesGenres = _seriesLogic.GetDesiredGenres();

            var homepageViewModel = new HomePageViewModel
            {
                MovieGenres = new List<MovieGenreViewModel>(),
                SeriesGenres = new List<SeriesGenreViewModel>()
            };

            // Fetch movie data for each genre and add it to the homepage view model
            foreach (var genre in movieGenres)
            {
                var movieGenre = await _movieLogic.GetMoviesByGenreAsync(genre.Id, genre.Name);
                if (movieGenre != null)
                {
                    homepageViewModel.MovieGenres.Add(movieGenre);
                }
            }

            // Fetch series data for each genre and add it to the homepage view model
            foreach (var genre in seriesGenres)
            {
                var seriesGenre = await _seriesLogic.GetSeriesByGenreAsync(genre.Id, genre.Name);
                if (seriesGenre != null)
                {
                    homepageViewModel.SeriesGenres.Add(seriesGenre);
                }
            }

            return homepageViewModel;
        }
    }
}

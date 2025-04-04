using Movie_website.Models;

namespace Movie_website.ViewModels
{
    public class HomePageViewModel
    {
        public List<MovieGenreViewModel> MovieGenres { get; set; }
        public List<SeriesGenreViewModel> SeriesGenres { get; set; }
        public List<Movie> LatestMovies { get; set; } // Add this new property

        public List<Video> MovieVideos { get; set; }
    }
}

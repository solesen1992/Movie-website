using Movie_website.Models;

/** 
 * HomePageViewModel
 * 
 * This model is used to represent the data required to render the homepage.
 * It includes lists of movies and series grouped by genre.
 * 
 * Why we have it: We need a single view model that contains all the data to display the homepage.
 * This allows for a clean and structured way to pass multiple lists (movies, series, videos) to the view.
 */

namespace Movie_website.ViewModels
{
    public class HomePageViewModel
    {
        public List<MovieGenreViewModel> MovieGenres { get; set; }
        public List<SeriesGenreViewModel> SeriesGenres { get; set; }
    }
}

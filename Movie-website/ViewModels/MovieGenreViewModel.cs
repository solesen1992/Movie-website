using Movie_website.Models;

/** 
 * MovieGenreViewModel
 * 
 * This model is used to represent a genre of movies and includes the movies within that genre.
 * It includes the genre ID, name, a list of movies, and the total number of movies in that genre.
 * 
 * Why we have it: This ViewModel allows us to display movie data grouped by genre on the homepage
 * or other pages where we want to list movies by genre. It also helps to manage pagination or filtering 
 * by providing the total count of movies in the genre.
 */

namespace Movie_website.ViewModels
{
    public class MovieGenreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Movie> Movies { get; set; }
        public int TotalCount { get; set; }
    }
}

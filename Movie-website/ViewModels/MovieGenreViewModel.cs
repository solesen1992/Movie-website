using Movie_website.Models;

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

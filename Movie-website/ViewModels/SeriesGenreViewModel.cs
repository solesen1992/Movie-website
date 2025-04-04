using Movie_website.Models;

namespace Movie_website.ViewModels
{
    public class SeriesGenreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Series> Series { get; set; }
        public int TotalCount { get; set; }
    }
}

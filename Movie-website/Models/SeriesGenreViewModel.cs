namespace Movie_website.Models
{
    public class SeriesGenreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Series> Series { get; set; }
        public int TotalCount { get; set; }
    }
}

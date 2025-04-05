using Movie_website.Models;

/** 
 * SeriesGenreViewModel
 * 
 * This model is used to represent a genre of TV series and includes the series within that genre.
 * It has the genre ID, name, a list of series, and the total count of series in that genre.
 * 
 * Why we have it: Just like MovieGenreViewModel, this ViewModel is used to display TV series 
 * grouped by genre. It helps with organizing the data and allows for the display of series 
 * alongside additional information like total counts.
 */

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

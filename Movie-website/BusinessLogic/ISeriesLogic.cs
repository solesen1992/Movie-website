using Movie_website.Models;
using Movie_website.ViewModels;

/* 
 * ISeriesLogic
 * 
 * This is an interface that defines the contract for the SeriesLogic class. It specifies what methods the SeriesLogic class must implement.
 * The interface acts as a blueprint for the logic layer that handles the business rules and data processing related to TV series.
 */

namespace Movie_website.BusinessLogic
{
    public interface ISeriesLogic
    {
        Task<SeriesGenreViewModel> GetSeriesByGenreAsync(int genreId, string genreName, int page = 1, bool isIndexPage = true);

        List<(int Id, string Name)> GetDesiredGenres();
        Task<Series> GetSeriesDetailsAsync(int id);
    }
}

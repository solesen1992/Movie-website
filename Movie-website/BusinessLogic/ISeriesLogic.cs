using Movie_website.Models;
using Movie_website.ViewModels;

namespace Movie_website.BusinessLogic
{
    public interface ISeriesLogic
    {
        Task<SeriesGenreViewModel> GetSeriesByGenreAsync(int genreId, string genreName, int page = 1, bool isIndexPage = true);

        List<(int Id, string Name)> GetDesiredGenres();
        Task<Series> GetSeriesDetailsAsync(int id);
    }
}

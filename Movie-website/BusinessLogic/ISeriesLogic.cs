using Movie_website.Models;

namespace Movie_website.BusinessLogic
{
    public interface ISeriesLogic
    {
        Task<SeriesGenreViewModel> GetSeriesByGenreAsync(int genreId, string genreName, int page = 1);

        List<(int Id, string Name)> GetDesiredGenres();
        Task<Series> GetSeriesDetailsAsync(int id);
    }
}

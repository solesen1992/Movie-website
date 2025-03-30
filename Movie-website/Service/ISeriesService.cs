using Movie_website.Models;

/*
 * ISeriesService
 * 
 * This interface defines the methods that any SeriesService class must implement.
 * It is used to fetch series data from the API.
 */

namespace Movie_website.Service
{
    public interface ISeriesService
    {
        Task<ApiListResponse<Series>> GetSeriesByGenreAsync(int genreId, int page = 1);
        Task<Series> GetSeriesDetailsAsync(int id);
    }
}

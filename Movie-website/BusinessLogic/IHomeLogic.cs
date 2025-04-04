using Movie_website.Models;

namespace Movie_website.BusinessLogic
{
    public interface IHomeLogic
    {
        Task<HomePageViewModel> GetHomePageDataAsync();
    }
}

using Movie_website.ViewModels;

namespace Movie_website.BusinessLogic
{
    public interface IHomeLogic
    {
        Task<HomePageViewModel> GetHomePageDataAsync();
    }
}

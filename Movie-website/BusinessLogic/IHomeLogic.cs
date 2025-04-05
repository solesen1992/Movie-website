using Movie_website.ViewModels;

/* 
 * IHomeLogic
 * 
 * This interface defines the methods required for handling business logic on the homepage.
 * It is used for fetching movies and series data by genres, and it serves as the contract for the HomeLogic class.
 * 
 * Why do we have it?
 * - It provides an abstraction for home page business logic. This logic is separated from the controllers.
 */

namespace Movie_website.BusinessLogic
{
    public interface IHomeLogic
    {
        Task<HomePageViewModel> GetHomePageDataAsync();
    }
}

# 🎬 Movie website
This project was made as a part of Wexo's code challenge.
I chose to build it using the MVC pattern (Model-View-Controller) because I have worked with it once before and wanted to challenge myself.

It was my first time fetching and working with data from an external API that I didn’t create myself, which was a great learning experience. My main focus was on figuring out how to connect to The Movie Database (TMDb) API, read the JSON data, and show the right information in the application.

## 🚀 How to open the file
Open the project in Visual Studio and run it in https mode.
This will start the website locally in your browser.

# 🖼️ Screenshot of website frontpage


# 📄 Documentation
Information about some of my coding choices.

## 🟢 Architecture
The project is built following the MVC pattern:

- Model → For domain models like Movie, Series, Genre

- View → For presenting the data in the frontend

- Controller → For handling HTTP requests

- Service Layer → For fetching and handling data from the API. The service layer is used to separate the logic of fetching API data from the controllers and follows Dependency Injection principles.

## 🔗 API connection
The project connects to The Movie Database API.
The connection details (API key and base URL) are stored in appsettings.json and read in the service layer.
This way: 

✅ Multiple service layers (MovieService & SeriesService) can share the same connection information

✅ It is easy to maintain — if the API key or URL changes, you only need to change it once in appsettings.json

The API documentation can be found here: https://developer.themoviedb.org/reference/intro/getting-started 

## 🌐 HttpClient and Dependency Injection
Dependency Injection (DI) is used to register and manage services such as MovieService and SeriesService. These services are responsible for communicating with The Movie Database API to fetch movie and series data.

To register these services, the project uses:

```
builder.Services.AddHttpClient<IMovieService, MovieService>();

builder.Services.AddHttpClient<ISeriesService, SeriesService>();
```

### Why AddHttpClient()?
ASP.NET Core provides a built-in HttpClient Factory (IHttpClientFactory), which is used here through .AddHttpClient(). It solves two common problems:

✅ Socket exhaustion
If you create a new HttpClient instance manually each time (using new HttpClient()), the application will eventually run out of available sockets because each client creates a new TCP connection. HttpClientFactory reuses connections behind the scenes, preventing this problem.

✅ DNS update issues
When an API's IP address changes (e.g., The Movie Database's server moves), a manually created HttpClient would continue using the old IP because it caches DNS information forever. HttpClientFactory automatically handles DNS updates by renewing connection handlers.

### Dependency Injection (DI) usage
The services are registered in Program.cs and then automatically injected into the controllers.

Example:
```
public HomeController(IMovieService movieService, ISeriesService seriesService)
{
    _movieService = movieService;
    _seriesService = seriesService; 
}
```

✅ You do not create objects manually with 'new MovieService()'

✅ Instead, ASP.NET Core’s DI container manages the lifetime and creation of these objects.

✅ Better separation of concerns and low coupling between layers.

## 📄 Interfaces
The project uses interfaces (e.g. IMovieService, ISeriesService) to create low coupling between layers.

This makes it possible to: 

✅ Change or extend the service layer without affecting controllers

✅ Make the application easier to test and maintain

## 🔄 Response-classes in the model layer & JSON handling
In the first version of the project, I created Response classes in the model layer (e.g. ApiListResponse<T>) to match the JSON structure from the API. However, I later chose to remove these classes and instead parse the JSON directly in the Service Layer using JsonDocument.

This way:

✅ The application is not dependent on the external API’s data structure

✅ Only the needed data is mapped manually to my own models

✅ It gives me full control over which fields are used

## 🎯 ModelView in the model layer
I also created simple ViewModels to prepare and structure the data before sending it to the Views.
This was my first time working with ViewModels.
It helped me keep the Views simple and clean and only show the data the user needs to see.

## Wishlist
This project includes a Wishlist feature. The wishlist is saved in the Session, meaning:

✅ It remembers which movies/series the user added

✅ The data is stored temporarily as long as the website is open and the session is active

The wishlist feature was a good exercise in working with Session state in ASP.NET Core.

## Things that could be changed
If I had more time, I could improve the project by:

- Adding a Business Logic Layer between the Controller and Service Layer → This would make the project even more structured and better follow the Separation of Concerns principle.

- Adding unit tests for the service layer and business logic.

- Adding error handling and user feedback in the Views if the API call fails.

- Making the wishlist persist between sessions (e.g. saving to a database instead of session).

- Using retry policies or timeout configurations in .AddHttpClient().

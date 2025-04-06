# 🎬 Movie website
This project was made as a part of Wexo's code challenge.
I chose to build it using the MVC pattern (Model-View-Controller) because I have worked with it once before and wanted to challenge myself.

It was my first time fetching and working with data from an external API that I didn’t create myself, which was a great learning experience. My main focus was on figuring out how to connect to The Movie Database API, read the JSON data, and show the right information in the application.

## 🚀 How to open the file
- Open the project in Visual Studio and run it in https mode.

- This will start the website locally in your browser.

# 🖼️ Screenshot of website frontpage
![Image](https://github.com/user-attachments/assets/8fe54c85-2cc3-4333-af52-04c8ad684ffc)

# 🖼️ Screenshot of movie details
<img width="1279" alt="Image" src="https://github.com/user-attachments/assets/277979c6-b70a-47e7-abac-6ac31dbc1949" />

# 📄 Documentation
Information about some of my coding choices.

## 🟢 Architecture
The project is built following the MVC pattern:

- Model → For domain models like Movie, Series, Genre
  
- ResponseModels → Represent data received from the API)
  
- ViewModels → Represent the data that is specifically formatted and structured for display in the user interface.

- View → For presenting the data in the frontend

- Controller → For handling HTTP requests

- Business Logic Layer → Processes and applies business rules to the data that is fetched or manipulated

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

## 🚀 Async vs sync

## 📄 Interfaces
The project uses interfaces (e.g. IMovieService, ISeriesService) to create low coupling between layers.

This makes it possible to: 

✅ Change or extend the service layer without affecting controllers

✅ Make the application easier to test and maintain

## 🔄 Response-classes in the model layer & JSON handling
In this project, I initially used custom Response classes like ApiListResponse<T>, CreditsListResponse, and VideoListResponse to deserialize the JSON returned by The Movie Database API. However, I decided to remove these response classes and instead handle the JSON manually in the Service Layer using JsonDocument and JsonSerializer.

For example my ApiListResponse<T> looked like this:
```
/*
 * This model represents the JSON structure returned by The Movie Database API when you request a list of movies or series.
 * It is a generic class, meaning it can be used for both movies and series (or anything else) because of <T>.
 * When you fetch movies, it will be ApiListResponse<Movie>. When you fetch series, it will be ApiListResponse<Series>
 * 
 * Why use [JsonPropertyName()]?
 * The API returns property names in snake_case (e.g. "total_results"). In C#, we normally use PascalCase (TotalResults).
 * So we use JsonPropertyName to tell the deserializer: "When you see 'total_results' in JSON, map it to 'TotalResults' in C#."
 */
public class ApiListResponse<T>
{
    [JsonPropertyName("results")]
    public List<T> Results { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }
}
```

✅ Why I made this choice

Better control:
- By reading and mapping the JSON manually, I have full control over which fields I want to use. I don’t depend on the exact structure of the API response.

Improved understanding:
- It forced me to work more directly with JSON structure, which gave me a better technical understanding of how data is transferred and parsed.

Cleaner models:
- I no longer need extra response models just to match the API's JSON format. Instead, I only use real domain models (Movie, Series, Credits, Genre, Video) which are used throughout my application.

Easier maintenance:
- If the API structure changes slightly, I only need to adjust how I parse the JSON — not rewrite response classes.

❗️ Tradeoff
- This approach requires slightly more code in the Service Layer and some manual null-checks, but for a smaller project like this, it increases transparency and reduces unnecessary complexity.

## 🎯 ModelView in the model layer
I also created simple ViewModels to prepare and structure the data before sending it to the Views.
This was my first time working with ViewModels.
It helped me keep the Views simple and clean and only show the data the user needs to see.

## 📄 Wishlist
This project includes a Wishlist feature. The wishlist is saved in the Session, meaning:

✅ It remembers which movies the user added

✅ The data is stored temporarily as long as the website is open and the session is active

The wishlist feature was a good exercise in working with Session state in ASP.NET Core.

## Error prevention on photos
Sometimes the movie/series poster and background photo aren't available. So I've inserted a stockphoto in case it doesn't find a photo.

Here's an example of some of the posters and backgrounds that didn't load:
<img width="500" alt="Image" src="https://github.com/user-attachments/assets/a924b26d-2529-49ac-9ab8-866088ac72fb" />

<img width="500" alt="Image" src="https://github.com/user-attachments/assets/1e440884-993d-493d-b57f-31bf193befd0" />

## Things that could be changed
If I had more time, I could improve the project by:

- Adding user feedback in the Views if the API call fails.

- Making the wishlist persist between sessions.

- Make it possible to add tv-series to the wishlist.

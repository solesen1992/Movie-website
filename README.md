# 🎬 Movie website
This project was made as a part of Wexo's code challenge.
I chose to build it using the MVC pattern (Model-View-Controller) because I have worked with it once before and wanted to challenge myself.

It was my first time fetching and working with data from an external API that I didn’t create myself, which was a great learning experience. My main focus was on figuring out how to connect to The Movie Database API, read the JSON data, and show the right information in the application.

## 🚀 How to open the file
- Open the project in Visual Studio and run it in https mode.

- This will start the website locally in your browser.

# 🖼️ Screenshot of website frontpage
<img width="700" alt="Image" src="https://github.com/user-attachments/assets/8fe54c85-2cc3-4333-af52-04c8ad684ffc" />

# 🖼️ Screenshot of movie details
<img width="700" alt="Image" src="https://github.com/user-attachments/assets/277979c6-b70a-47e7-abac-6ac31dbc1949" />

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

## 🔄 Response-classes & JSON handling
In this project, I chose to use response classes like ApiListResponse<T>, CreditsListResponse, and VideoListResponse to deserialize JSON returned from The MovieDatabase API.

I made this decision because it's my first time working with response classes, and I wanted to try them out in practice and see how they work.

✅ Why I chose to keep response classes

- Easy structure: Each response class represents the JSON shape returned by the API, making it simple to deserialize directly using GetFromJsonAsync<T>().

- Less boilerplate: Instead of manually digging through JSON with JsonDocument, I can let .NET map everything automatically.

- Good for learning: Since I'm new to this pattern, using response classes helps me see how data is structured in the API and how it maps to C# models.

🧠 Alternative approach: Manual JSON mapping

- Alternatively, I could have skipped response classes and mapped the raw JSON manually in the Service Layer using JsonDocument or JsonNode. This would have given me more control and a deeper understanding of the JSON structure.

Here’s an example of how I could have mapped the JSON myself:
```
public async Task<List<Movie>> GetMoviesByGenreAsync(int genreId, int page = 1)
{
    try
    {
        // Build the API request URL using the selected genre and page number
        string url = $"{_baseUrl}discover/movie?api_key={_apiKey}&with_genres={genreId}&page={page}";

        // Send an HTTP GET request to the API
        var httpResponse = await _httpClient.GetAsync(url);

        // If the response is not successful (e.g. 404 or 500), return an empty list
        if (!httpResponse.IsSuccessStatusCode)
        {
            return new List<Movie>();
        }

        // Read the response content as a JSON string
        var jsonString = await httpResponse.Content.ReadAsStringAsync();

        // Parse the JSON into a JsonDocument for manual processing
        using var document = JsonDocument.Parse(jsonString);
        var root = document.RootElement;

        // Create a list to store the mapped Movie objects
        var movies = new List<Movie>();

        // Loop through each movie in the "results" array
        foreach (var item in root.GetProperty("results").EnumerateArray())
        {
            // Map only the fields we need from each JSON object
            var movie = new Movie
            {
                Id = item.GetProperty("id").GetInt32(),
                Title = item.GetProperty("title").GetString(),
                Overview = item.GetProperty("overview").GetString(),
                PosterPath = item.TryGetProperty("poster_path", out var poster) ? poster.GetString() : null,
                ReleaseDate = item.TryGetProperty("release_date", out var release) ? release.GetString() : null
                // You can add more fields as needed
            };

            // Add the mapped movie to the list
            movies.Add(movie);
        }

        // Return the final list of movies
        return movies;
    }
    // Catch is removed from example
}
```
Manual JSON handling would have given me:
- More control over exactly what I fetch
- Better understanding of JSON structure and deserialization
- But it also requires more code and error handling

For this project, response classes made sense – but trying both ways helped me learn a lot about how .NET works with JSON.

## 🎯 ModelView in the model layer
I also created simple ViewModels to prepare and structure the data before sending it to the Views.
This was my first time working with ViewModels.
It helped me keep the Views simple and clean and only show the data the user needs to see.

## 📄 Wishlist
This project includes a Wishlist feature. The wishlist is saved in the Session, meaning:

✅ It remembers which movies the user added

✅ The data is stored temporarily as long as the website is open and the session is active

The wishlist feature was a good exercise in working with Session state in ASP.NET Core.

## 🧠 Error prevention on photos
Sometimes the movie/series poster and background photo aren't available. So I've inserted a stockphoto in case it doesn't find a photo.

Here's an example of some of the posters and backgrounds that didn't load:
<img width="500" alt="Image" src="https://github.com/user-attachments/assets/a924b26d-2529-49ac-9ab8-866088ac72fb" />

<img width="500" alt="Image" src="https://github.com/user-attachments/assets/1e440884-993d-493d-b57f-31bf193befd0" />

## Things that could be changed
If I had more time, I could improve the project by:

- Adding user feedback in the Views if the API call fails.

- Making the wishlist persist between sessions.

- Make it possible to add tv-series to the wishlist.

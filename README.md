# Movie website
This code was made as a part of Wexo's code challenge. I choose to make it in MVC that I've worked with once before. First time trying to get information from an API, I haven't made, which was a challenge so my focus was on trying to get the right information from the movie database.

## How to open the file
Open the code in Visual Studio and run it in https. This should open the website.

# Screenshot of website frontpage


# Documentation
Information about some of my coding choices.

## Architecture
MVC - Controller, View, Model, Service layer

## API connection
Connection through appsettings.json so it can be used by more than one servicelayer. It makes it easier to vedligeholde so if the api connection ever needs to be changed, I just have to change it one place instead of multiple service-layers.

The documentation for the middleware API can be found here: https://developer.themoviedb.org/reference/intro/getting-started 

## HttpClient and Dependency Injection
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

This improves separation of concerns and gives low coupling between the layers.

## Interfaces
To make low coupling between the layers. So you can change something from the servicelayer without affection the other code.

## Response-classes in the model layer
Alternative is to handle the json in the servicelayer.
First time trying it.

## ModelView in the model layer
First time doing it.

## Wishlist
First time doing it. Saves in a session - so as long the website is running, it saves the changes.

## Things that could be changed
- I could have added a BusinessLogicLayer between the Controller and ServiceLayer. That would be better for separations of concerns.

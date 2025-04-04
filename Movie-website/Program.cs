using Movie_website.BusinessLogic;
using Movie_website.Models;
using Movie_website.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

/*
 * HttpClient Factory Explanation
 * 
 * MovieService and SeriesService are registered using AddHttpClient(). 
 * This automatically injects an HttpClient into MovieService and SeriesService through Dependency Injection (DI).
 * It uses ASP.NET Core's built-in IHttpClientFactory, which is a factory pattern 
 * that manages the creation and lifetime of HttpClient instances.
 * 
 * Benefits:
 * - Prevents socket exhaustion by reusing underlying HTTP connections (via HttpMessageHandler pooling) instead of opening a new TCP connection each time.
 * - Handles DNS updates correctly by recycling handlers when needed.
 * 
 * Note:
 * Even though a new HttpClient instance can be injected per request, IHttpClientFactory ensures that the underlying connections are reused.
 * ---------------------------------
 * Dependency Injection explanation
 * 
 * Dependency Injection is used instead of creating objects manually with 'new' in each controller. 
 * The DI container (built into ASP.NET Core) automatically creates and manages the lifetime of MovieService and SeriesService.
 */
// Register MovieService and SeriesService
builder.Services.AddHttpClient<IMovieService, MovieService>(); // Added so it only uses one HttpClient per service.
builder.Services.AddHttpClient<ISeriesService, SeriesService>();

// Register Business Logic Layer services
builder.Services.AddScoped<IMovieLogic, MovieLogic>();
builder.Services.AddScoped<ISeriesLogic, SeriesLogic>();
builder.Services.AddScoped<IHomeLogic, HomeLogic>();

builder.Services.AddSession(); // Added for session support

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession(); // Registers middleware and reads and writes cookies

//UseSession is before Routing and Authorization because they can be affected by session state
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

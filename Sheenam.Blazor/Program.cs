using Blazored.LocalStorage;
using Sheenam.Blazor.Brokers.Apis;
using Sheenam.Blazor.Brokers.Loggings;
using Sheenam.Blazor.Data;
using Sheenam.Blazor.Services.Foundations.Auth;
using Sheenam.Blazor.Services.Foundations.Guests;
using Sheenam.Blazor.Services.Foundations.HomeRequests;
using Sheenam.Blazor.Services.Foundations.Homes;
using Sheenam.Blazor.Services.Foundations.Hosts;
using Sheenam.Blazor.Services.Foundations.Reviews;
using Sheenam.Blazor.Services.Foundations.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddBlazoredLocalStorage();


builder.Services.AddHttpClient<IApiBroker, ApiBroker>(client =>
{
    var apiSettings = builder.Configuration.GetSection("ApiSettings");
    client.BaseAddress = new Uri(apiSettings["BaseUrl"]);
});
builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();

builder.Services.AddTransient<IGuestService, GuestService>();
builder.Services.AddTransient<IHomeService, HomeService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IHostService, HostService>();
builder.Services.AddTransient<IHomeRequestService, HomeRequestService>();
builder.Services.AddTransient<IReviewService, ReviewService>();
builder.Services.AddTransient<IAuthService, AuthService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

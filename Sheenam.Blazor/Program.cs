using Microsoft.AspNetCore.Components.Authorization;
using Sheenam.Blazor.Components;
using Sheenam.Blazor.Services;
using Sheenam.Blazor.Services.Brokers;
using Sheenam.Blazor.Services.Foundations.Guests;
using Sheenam.Blazor.Services.Views.Guests;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure HttpClient with base address
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7001/")
});

// Register Authentication
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TokenStorageService>();
builder.Services.AddAuthorizationCore();

// Register Brokers
builder.Services.AddScoped<IApiBroker, ApiBroker>();

// Register Foundation Services
builder.Services.AddScoped<IGuestService, GuestService>();

// Register View Services
builder.Services.AddScoped<IGuestViewService, GuestViewService>();

// Register other services
builder.Services.AddScoped<ToastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

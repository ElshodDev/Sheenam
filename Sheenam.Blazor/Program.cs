//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Components;
using Sheenam.Blazor.Services;

namespace Sheenam.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddHttpClient();

            builder.Services.AddSingleton(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var baseUrl = config["ApiSettings:BaseUrl"] ?? "https://localhost:5001/";

                Console.WriteLine($"🌐 API Base URL: {baseUrl}");

                return new HttpClient
                {
                    BaseAddress = new Uri(baseUrl)
                };
            });

            builder.Services.AddScoped<ToastService>();
            builder.Services.AddScoped<HomeRequestService>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseAntiforgery();
            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
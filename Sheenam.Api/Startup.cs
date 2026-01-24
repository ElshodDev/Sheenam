//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.MachineLearning;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Services.AI.Recommendations;
using Sheenam.Api.Services.Foundations.Auth;
using Sheenam.Api.Services.Foundations.Guests;
using Sheenam.Api.Services.Foundations.HomeRequests;
using Sheenam.Api.Services.Foundations.Homes;
using Sheenam.Api.Services.Foundations.Hosts;
using Sheenam.Api.Services.Foundations.PropertySales;
using Sheenam.Api.Services.Foundations.SaleOffers;
using Sheenam.Api.Services.Foundations.Users;
using System.Text;

namespace Sheenam.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<StorageBroker>();

            AddBrokers(services);
            AddFoundationService(services);
            AddAIServices(services);

            AddJwtAuthentication(services);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazor", builder =>
                {
                    builder
                        .WithOrigins(
                            "http://localhost:5184",
                            "https://localhost:5185",
                            "http://localhost:5000",
                            "https://localhost:5001"
                        )
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sheenam.Api",
                    Version = "v1",
                    Description = "Home rental API with JWT Authentication & AI Recommendations"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint(
                       url: "/swagger/v1/swagger.json",
                       name: "Sheenam.Api v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors("AllowBlazor");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                endpoints.MapControllers());
        }

        private static void AddBrokers(IServiceCollection services)
        {
            services.AddTransient<IStorageBroker, StorageBroker>();
            services.AddTransient<ILoggingBroker, LoggingBroker>();
        }

        private static void AddAIServices(IServiceCollection services)
        {
            services.AddSingleton<IMLBroker, MLBroker>();

            services.AddTransient<IRecommendationService, RecommendationService>();
        }

        private static void AddFoundationService(IServiceCollection services)
        {
            services.AddTransient<IGuestService, GuestService>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<IHostService, HostService>();
            services.AddTransient<IHomeRequestService, HomeRequestService>();
            services.AddTransient<IPropertySaleService, PropertySaleService>();
            services.AddTransient<ISaleOfferService, SaleOfferService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthService>();
        }

        private void AddJwtAuthentication(IServiceCollection services)
        {
            var jwtSettings = Configuration.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey)
                };
            });

            services.AddAuthorization();
        }
    }
}
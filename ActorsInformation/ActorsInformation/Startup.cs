using ActorsInformation.Data;
using ActorsInformation.Middleware;
using ActorsInformation.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ActorsInformation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDataContext>(opt => opt.UseInMemoryDatabase("ActorsList"));
            services.AddHttpClient();

            services.AddControllers();
            services.AddAuthorization();

            services.AddScoped<IMoviesScraper, ImdbMovieScraper>();
            services.AddScoped<IActorService, ActorService>();
            services.AddTransient<IApiKeyValidation, ApiKeyValidation>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Top actors information",
                    Description = "C# scraper Api to retrieve information about the top actors from IMDb Within RESTful API",
                    Contact = new OpenApiContact
                    {
                        Name = "Lital R",
                        Email = "litalrab@gmail.comm"
                    },
                });

                options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Description = "ApiKey must appear in header",
                    Type = SecuritySchemeType.ApiKey,
                    Name = "XApiKey",
                    In = ParameterLocation.Header,
                    Scheme = "ApiKeyScheme"
                });

                var key = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    },
                    In = ParameterLocation.Header
                };

                var requirement = new OpenApiSecurityRequirement
                    {
                             { key, new List<string>() }
                    };
                options.AddSecurityRequirement(requirement);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<ApiKeyMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {

                var moviesScraperService = scope.ServiceProvider.GetRequiredService<IActorService>();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDataContext>();
                dbContext.Database.EnsureCreated();
                moviesScraperService.LoadActors();
            }

        }
    }
}

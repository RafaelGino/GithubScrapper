using GithubWebScrapper._1___Services.Implementations;
using GithubWebScrapper._1___Services.Interfaces;
using GithubWebScrapper._2___Domain.Repositories;
using GithubWebScrapper._3___Infra;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace GithubWebScrapper
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WebScraping Github Api",
                    Description = "Api to get lines and bytes grouped by extension from a given github page.",
                    TermsOfService = new Uri("https://github.com/RafaelGino"),
                    Contact = new OpenApiContact
                    {
                        Name = "Rafael Pelicioni",
                        Email = "rafaelpelicione@gmail.com"                        
                    }

                });
            });
            //Dependency Injection
            services.AddScoped<IScrapperService, ScrapperService>();
            services.AddScoped<IScrapperRepository, ScrapperRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebScraping GitHub Api");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

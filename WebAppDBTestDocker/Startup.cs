using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebAppDBTestDocker.Models;

namespace WebAppDBTestDocker
{
    public class Startup
    {
        //NOTE: использую Configuration типа IConfigurationRoot вместо IConfiguration
        // это нужно ТОЛЬКО для того, чтобы вывести ID контейнера (береться из config["HOSTNAME"]) 
        // ID контейнера выводится для визуального тестирования балансировщика нагрузки, чтобы видеть  
        // что при каждом обновлении страницы выводятся разные ID.
        //
        // Изменения, которые сделаны для этого, не нужно переновить в продакшн в таких файлах:
        // - Program.cs
        // - Startup.cs
        // - HomeController.cs

        private IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables()
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = @"Server=db;Database=master;User Id=sa;Password=Pass!2017;";
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connection));

            services.AddMvc();

            services.AddSingleton(Configuration);
            services.AddTransient<IUserRepository, EFUserRepository>();
            services.AddTransient<IGuidRepository, EFGuidRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            SeedData.EnsurePopulated(app.ApplicationServices);
        }
    }
}

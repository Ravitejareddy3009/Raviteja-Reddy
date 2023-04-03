using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Framework.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebApplication
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json");
                      
            var configuration = builder.Build();

            services.AddMvc();
            services.AddEntityFramework()
            .AddDbContext<EntityModelContext>(
                options => options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();

            app.Run(context =>
            {
                return context.Response.WriteAsync("");
            });
        }
    }
}
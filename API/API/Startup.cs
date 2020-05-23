using API.Middleware;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Application.Users;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API
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
            services.AddDbContext<DataContext>(options => {
              options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddCors(options =>{
              options.AddPolicy("CorsPolicy", policy => {
                policy.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:4200");
              });
            });

            services.AddMediatR(typeof(List.Handler).Assembly); //one is enough
            services.AddControllers();
        }

   
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseMiddleware<ErrorHandlingMiddleware>();
      app.UseRouting();
      app.UseCors("CorsPolicy");
      app.UseAuthorization();

      app.UseStaticFiles(new StaticFileOptions()
      {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
        RequestPath = "/Resources"
      });

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}

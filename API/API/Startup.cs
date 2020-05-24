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
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using FluentValidation.AspNetCore;
using Application.Interfaces;
using Infrastructure.Security;
using Domain;
using Microsoft.OpenApi.Models;
using System.Reflection;

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
      services.AddDbContext<DataContext>(options =>
      {
        options.UseLazyLoadingProxies();
        options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
      });

      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy", policy =>
        {
          policy.AllowAnyHeader()
          .AllowAnyMethod()
          .WithExposedHeaders("WWW-Authenticate")
          .WithOrigins("http://localhost:4200")
          .AllowCredentials();
        });
      });

      services.AddMediatR(typeof(List.Handler).Assembly);
      services.AddAutoMapper(typeof(List.Handler));


      services.AddAuthorization(options =>
      {
        options.AddPolicy("RequireAdministratorRole",
           policy => policy.RequireRole(Roles.Admin));
      });

      services.AddMvc().AddFluentValidation(cfg =>
      {
        cfg.RegisterValidatorsFromAssemblyContaining<Create>();
      });

      services.AddDefaultIdentity<AppUser>()
      .AddEntityFrameworkStores<DataContext>()
      .AddSignInManager<SignInManager<AppUser>>();

      services.Configure<IdentityOptions>(opt =>
      {
        opt.Password.RequireDigit = false;
        opt.Password.RequiredLength = 4;
        opt.Password.RequireNonAlphanumeric = false;
        opt.Password.RequireUppercase = false;
      });

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"]));
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = key,
          ValidateAudience = false,
          ValidateIssuer = false,
          ValidateLifetime = true,
          ClockSkew = TimeSpan.Zero
        };

      });

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "User Manager API",
          Description = "A simple .NET Core Web API",
          Version = "v1"
        });

        OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
        {
          Name = "Bearer",
          BearerFormat = "JWT",
          Scheme = "bearer",
          Description = "Specify the authorization token.",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.Http,
        };
        c.AddSecurityDefinition("jwt_auth", securityDefinition);

        // Make sure swagger UI requires a Bearer token specified
        OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
        {
          Reference = new OpenApiReference()
          {
            Id = "jwt_auth",
            Type = ReferenceType.SecurityScheme
          }
        };

        OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
        {
            {securityScheme, new string[] { }},
        };
        c.AddSecurityRequirement(securityRequirements);

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
      });


      services.ConfigureSwaggerGen(options =>
     {
       
       options.CustomSchemaIds(x => x.FullName);
     });

      services.AddScoped<IJwtGenerator, JwtGenerator>();
    }


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseMiddleware<ErrorHandlingMiddleware>();

      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
      });

      app.UseDefaultFiles();
      app.UseStaticFiles(new StaticFileOptions()
      {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
        RequestPath = "/Resources"
      });

      app.UseRouting();
      app.UseCors("CorsPolicy");

      app.UseAuthentication();
      app.UseAuthorization();


      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}

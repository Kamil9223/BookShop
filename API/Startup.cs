using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using FluentValidation.AspNetCore;
using API.MiddleWares;
using Core.IRepositories;
using DatabaseAccess.MSSQL_BookShop;
using AuthService.Repositories;
using AuthService.Services.Interfaces;
using AuthService.Services.Implementations;
using BookService.Repositories;
using OrderService.Repositories;
using BookService.Services.Interfaces;
using OrderService.OrderServices.Interfaces;
using OrderService.OrderServices.Implementations;
using OrderService.Helpers;
using API.Infrastructure;
using Unity.Microsoft.DependencyInjection;
using AuthService.Infrastructure;
using Microsoft.AspNetCore.Mvc.Controllers;
using Unity;
using BookService.Infrastructure;
using OrderService.Infrastructure;

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
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidIssuer = Configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                RequireExpirationTime = true
            };
            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });
            services.AddAuthorization(p => p.AddPolicy("admin", pol => pol.RequireRole("admin")));
            services.AddMemoryCache();

            services.AddCors(
                options => options.AddPolicy("AllowCors",
                builder =>
                {
                    builder
                    //.AllowAnyOrigin()
                    .WithOrigins("http://localhost:4200")
                    .WithMethods("GET", "PUT", "POST", "DELETE")
                    .AllowAnyHeader();
                })
            );
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });
            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationMiddleWare>();
            })
                .AddControllersAsServices()
                .AddFluentValidation(conf => conf.RegisterValidatorsFromAssemblyContaining<Startup>());

            var unityContainer = ContainerCreator.Instance();

            services.AddDbContext<BookShopContext>(o => o.UseSqlServer(Configuration["ConnectionString:BookShopDB"]));
            services.AddTransient<JwtTokenMiddleWare>();
            services.AddTransient<ErrorsMiddleWare>();

            //Services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IHttpSessionWrapper, HttpSessionWrapper>();

            services.AddTransient<IControllerActivator, UnityControllersActivator>();

            var unityProvider = services.BuildServiceProvider(unityContainer);

            AuthServiceStartup.RegisterServices(unityContainer);
            BookServiceStartup.RegisterServices(unityContainer);
            OrderServiceStartup.RegisterServices(unityContainer);

            return unityProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowCors");
            app.UseSession();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMiddleware<JwtTokenMiddleWare>();
            app.UseMiddleware<ErrorsMiddleWare>();
            app.UseMvc();
        }
    }
}

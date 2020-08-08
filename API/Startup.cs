using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using FluentValidation.AspNetCore;
using Infrastructure.DB;
using Infrastructure.IServices;
using API.MiddleWares;
using Core.IRepositories;
using Infrastructure.Helpers;

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
                .AddFluentValidation(conf => conf.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddDbContext<BookShopContext>(o => o.UseSqlServer(Configuration["ConnectionString:BookShopDB"]));
            services.AddTransient<JwtTokenMiddleWare>();
            services.AddTransient<ErrorsMiddleWare>();
            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ILoggedUserRepository, LoggedUserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();
            services.AddSingleton<IEncrypter, Encrypter>();
            services.AddSingleton<IJwtService, JwtService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICart, Cart>();
            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddScoped<IHttpSessionWrapper, HttpSessionWrapper>();
            services.AddScoped<IOrderService, OrderService>();
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

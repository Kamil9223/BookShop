using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ksiegarnia.DB;
using Microsoft.EntityFrameworkCore;
using Ksiegarnia.IRepositories;
using Ksiegarnia.Repositories;
using Ksiegarnia.Services;
using Ksiegarnia.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using Ksiegarnia.MiddleWares;

namespace Ksiegarnia
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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            };
            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });
            services.AddAuthorization(p => p.AddPolicy("admin", pol => pol.RequireRole("admin")));
            services.AddMemoryCache();
            //services.AddDistributedRedisCache(x => { x.Configuration = Configuration["Redis:ConnectionString"]; });
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
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(10); 
            });
            services.AddMvc();
            services.AddDbContext<BookShopContext>(o => o.UseSqlServer(Configuration["ConnectionString:BookShopDB"]));
            services.AddTransient<JwtTokenMiddleWare>();
            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<ITypeCategoryRepository, TypeCategoryRepository>();
            services.AddScoped<ILoggedUserRepository, LoggedUserRepository>();
            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();
            services.AddSingleton<IEncrypter, Encrypter>();
            services.AddSingleton<IJwtService, JwtService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICart, Cart>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors("AllowCors");
            app.UseSession();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMiddleware<JwtTokenMiddleWare>();

            app.UseMvc();
        }
    }
}

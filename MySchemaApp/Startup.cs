using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySchemaApp.Models.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySchemaApp
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
            services.AddControllersWithViews();
            services.AddMvc(opts =>  opts.EnableEndpointRouting = false);
           
            services.AddDbContext<AppIdentityDbContext>(opts =>
            opts.UseSqlServer(Configuration["ConnectionStrings:DefaultConnectionString"]));
            services.AddIdentity<AppUser, IdentityRole>(opts => {
                opts.Password.RequiredLength = 8;
                opts.Password.RequireLowercase = false;
                opts.User.AllowedUserNameCharacters = "abcçdefgðhiýjklmnöopqrstuüvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-. _ ";
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
                opts.User.RequireUniqueEmail = false;




            }).AddEntityFrameworkStores<AppIdentityDbContext>();
            CookieBuilder cookieBuilder = new CookieBuilder();
            cookieBuilder.Name = "MyBlog";
            cookieBuilder.HttpOnly = false;
            cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;

            cookieBuilder.SameSite = SameSiteMode.Strict;
            services.ConfigureApplicationCookie(opts =>
            {
                opts.Cookie = cookieBuilder;
                opts.LoginPath = new PathString("/Home/Login");
                opts.LogoutPath = new PathString("/Member/LogOut");
                opts.SlidingExpiration = true;
                opts.ExpireTimeSpan = System.TimeSpan.FromDays(60);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseStatusCodePages();
            app.UseMvcWithDefaultRoute();

        }
    }
}

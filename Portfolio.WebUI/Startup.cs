using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portfolio.Domain.Models.DataContext;
using MediatR;
using Portfolio.Domain.Models.Entities.Membership;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Portfolio.Domain.AppCode.Services;

namespace Portfolio.WebUI
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(cfg =>
            {
                var policyRule = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser().Build();
                cfg.Filters.Add(new AuthorizeFilter(policyRule));
            });
            services.AddRouting(cfg =>
            {
                cfg.LowercaseUrls = true;
            });
            services.AddDbContext<PortfolioDbContext>(cfg =>
            {
                cfg.UseSqlServer(configuration.GetConnectionString("portfolioConnectionString"));
            });
            services.AddIdentity<PortfolioUser, PortfolioRole>()
                .AddEntityFrameworkStores<PortfolioDbContext>();
            services.Configure<IdentityOptions>(cfg =>
            {
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredLength = 3;
                cfg.Password.RequiredUniqueChars = 1;

                cfg.SignIn.RequireConfirmedPhoneNumber = false;
                cfg.SignIn.RequireConfirmedAccount = false;
                cfg.SignIn.RequireConfirmedEmail = true;

                cfg.Lockout.MaxFailedAccessAttempts = 3;
                cfg.User.RequireUniqueEmail = true;
            });
            services.ConfigureApplicationCookie(cfg =>
            {
                cfg.Cookie.Name = "portfolio";
                cfg.LoginPath = "/signin.html";
                cfg.LogoutPath = "/logout.html";
                cfg.AccessDeniedPath = "/accessdenied.html";

                cfg.ExpireTimeSpan = new TimeSpan(0, 2, 0);
                cfg.Cookie.HttpOnly = true;//js mudaxile ede bilmesin deye
            });

            services.AddScoped<UserManager<PortfolioUser>>();
            services.AddScoped<SignInManager<PortfolioUser>>();
            services.AddScoped<RoleManager<PortfolioRole>>();
            services.Configure<EmailServiceOptions>(cfg =>
            {
                configuration.GetSection("emailAccount").Bind(cfg);
            });
            services.AddSingleton<EmailService>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.StartsWith("Portfolio.")).ToArray();
            services.AddMediatR(assemblies);
        }

       public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.SeedMembership();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(name: "defaultAdmin",
                    areaName: "Admin",
                    pattern: "admin/{controller=home}/{action=index}/{id?}");

                endpoints.MapControllerRoute(name: "default",
                    pattern: "{controller=home}/{action=index}/{id?}");
            });
        }
    }
}

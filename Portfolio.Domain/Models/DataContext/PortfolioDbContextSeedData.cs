using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Domain.Models.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Models.DataContext
{
    public static class PortfolioDbContextSeedData
    {
        public static IApplicationBuilder SeedMembership(this IApplicationBuilder app)
        {
            const string adminEmail = "ashrafsg@code.edu.az";
            const string adminUserName = "Ashraf";
            const string adminPassword = "esref2001";
            const string roleName = "sa";

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
                db.Database.Migrate();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<PortfolioUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<PortfolioRole>>();

                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new PortfolioRole
                    {
                        Name = roleName
                    };
                    roleManager.CreateAsync(role).Wait();
                }
                var user = userManager.FindByEmailAsync(adminEmail).Result;
                if (user==null)
                {
                    user = new PortfolioUser
                    {
                        Email = adminEmail,
                        UserName=adminUserName,
                        EmailConfirmed=true
                    };
                    userManager.CreateAsync(user,adminPassword).Wait();

                }
                if (userManager.IsInRoleAsync(user,roleName).Result==false)
                {
                    userManager.AddToRoleAsync(user,roleName).Wait();
                }
            }
            return app;
        }
    }
}

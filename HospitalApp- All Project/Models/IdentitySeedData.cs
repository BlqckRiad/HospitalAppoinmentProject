using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HospitalApp.Models;
using HospitalApp;

namespace HospitalApp.Models
{
    public class IdentitySeedData
    {
        private const string adminUser = "admin";
        private const string adminPass = "Admin_123";

        public static async void IdentityTestUser(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IdentityContext>();

            if (context.Database.GetAppliedMigrations().Any())
            {
                context.Database.Migrate();
            }

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var user = await userManager.FindByNameAsync(adminUser);

            if (user == null)
            {
                user = new AppUser
                {
                    FullName = "Enes Özbuğanlı",
                    UserName = adminUser,
                    Email = "admin@enes.com",
                    PhoneNumber = "123"
                };

                await userManager.CreateAsync(user, adminPass);
            }
        }
    }
}

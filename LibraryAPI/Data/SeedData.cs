using IdentityServer.Data;
using IdentityServer.Models;
using LibraryAPI.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LibraryAPI.Data
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using  var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var adminSettings = scope.ServiceProvider.GetRequiredService<IOptions<AdminUserSettings>>();

            var db = scope.ServiceProvider.GetRequiredService<IdentityServerDbContext>();
            var database = db.Database;
            db.Database.Migrate();

            // Seedig admin user
            await SeedRolesAsync(roleManager);
            await SeedAdminUserAsync(userManager, roleManager, adminSettings.Value);
        }



        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            List<string> Roles =
            [
                "Admin",
                "User",
                "Librarian",
            ];
            foreach (var role in Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        // seed admin user method
        public static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager , AdminUserSettings adminSettings)
        {
           var adminUser = await userManager.FindByNameAsync(adminSettings.Username);
            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = adminSettings.Username,
                    Email = adminSettings.Email
                };
                var result = await userManager.CreateAsync(user, adminSettings.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, adminSettings.Role);
                }
            }
        }
    }
}

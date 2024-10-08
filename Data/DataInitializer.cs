using HRM.Models;
using Microsoft.AspNetCore.Identity;

namespace HRM.Data
{
    public static class DataInitializer
    {
        public static async Task SeedUserAndRoleAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles Section
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync(AppUserRole.Admin))
                    await roleManager.CreateAsync(new IdentityRole(AppUserRole.Admin));
                if (!await roleManager.RoleExistsAsync(AppUserRole.User))
                    await roleManager.CreateAsync(new IdentityRole(AppUserRole.User));
                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                const string adminUsername = "admin";
                var adminUser = await userManager.FindByNameAsync(adminUsername);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "admin",
                    };
                    await userManager.CreateAsync(newAdminUser, "D@ngDuong0402");
                    await userManager.AddToRoleAsync(newAdminUser, AppUserRole.Admin);
                }
            }
        }
    }
}

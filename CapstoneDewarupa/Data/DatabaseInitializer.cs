using Microsoft.AspNetCore.Identity;

namespace CapstoneDewarupa.Data
{
    public class DatabaseInitializer
    {
        public static async Task SeedDataAsync(UserManager<IdentityUser>? userManager, RoleManager<IdentityRole>? roleManager)
        {
            if(userManager == null || roleManager == null)
            {
                Console.WriteLine("UserManager or RoleManager is null => exit");
                return;
            }

            var exists = await roleManager.RoleExistsAsync("admin");
            if(!exists)
            {
                Console.WriteLine("Admin role is not defined and will be created");
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            exists = await roleManager.RoleExistsAsync("client");
            if(exists)
            {
                Console.WriteLine("Client role is not defined and will be created");
                await roleManager.CreateAsync(new IdentityRole("client"));
            }

            var adminUsers = await userManager.GetUsersInRoleAsync("admin");
            if(adminUsers.Any())
            {
                Console.WriteLine("Admin user already exists => exit");
                return;
            }

            var user = new IdentityUser
            {
                UserName = "admin@mail.com",
                Email = "admin@mail.com"
            };

            string defaultPassword = "Admin123@";

            var result = await userManager.CreateAsync(user, defaultPassword);
            if(result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "admin");
                Console.WriteLine("Admin user created");
                Console.WriteLine("Email: " + user.Email + " - Initial Password : " + defaultPassword);
            }
            else
            {
                Console.WriteLine("Failed to create admin user: " + result.Errors.First().Description);
            }
        }
    }
}

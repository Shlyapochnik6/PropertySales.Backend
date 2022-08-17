using Microsoft.AspNetCore.Identity;
using PropertySales.Domain;

namespace PropertySales.Persistence.Initializers;

public class RoleInitializer
{
    public static async Task CreateAdminUser(RoleManager<IdentityRole<long>> roleManager,
        UserManager<User> userManager)
    {
        if (await roleManager.FindByNameAsync("Admin") == null)
        {
            await roleManager.CreateAsync(new IdentityRole<long>("Admin"));
        }
        
        var adminUser = new
        {
            UserName = "Admin",
            Email = "adminuser@gmail.com",
            Password = "1234567890"
        };
        
        var userAdmin = await userManager.FindByEmailAsync(adminUser.Email);
        if (userAdmin == null)
        {
            var user = new User()
            {
                UserName = adminUser.UserName,
                Email = adminUser.Email
            };
            await userManager.CreateAsync(user, adminUser.Password);
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
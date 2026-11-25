using Microsoft.AspNetCore.Identity;

namespace VeterinerKlinigiApp.Data;

public static class DbSeeder
{
    public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string[] roles = { "Admin", "Veteriner", "Üye" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // 1. Zorunlu Admin Hesabı
        await CreateUserWithRoleAsync(userManager, "b241200072@sakarya.edu.tr", "sau", "Admin");

        // 2. Test için Örnek Veteriner Hesabı
        await CreateUserWithRoleAsync(userManager, "veteriner@minikpatiler.com", "sau", "Veteriner");

        // 3. Test için Örnek Üye Hesabı
        await CreateUserWithRoleAsync(userManager, "uye@minikpatiler.com", "sau", "Üye");
    }

    private static async Task CreateUserWithRoleAsync(
        UserManager<IdentityUser> userManager,
        string email,
        string password,
        string role)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            user = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}

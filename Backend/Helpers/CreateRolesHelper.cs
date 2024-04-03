using Backend.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Backend.Helpers
{
    public static class CreateRolesHelper
    {
        public static async Task CreateDefaultRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var roles = new[] { "Admin", "User" };
            
            foreach (var role in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }
            
            var admin = new User
            {
                UserName = configuration["AppSettings:UserName"],
                Email = configuration["AppSettings:Email"],
            };

            var userPwd = configuration["AppSettings:Password"];
            var user = await userManager.FindByEmailAsync(admin.Email);

            if(user == null)
            {
                var createAdmin = await userManager.CreateAsync(admin, userPwd);
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
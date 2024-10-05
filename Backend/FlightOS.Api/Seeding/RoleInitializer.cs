using Microsoft.AspNetCore.Identity;

namespace FlightOS.Api.Seeding
{
    /// <summary>
    /// Provides methods to initialize roles in the application.
    /// </summary>
    public class RoleInitializer
    {
        /// <summary>
        /// Seeds the roles if they do not already exist.
        /// </summary>
        /// <param name="roleManager">The role manager to manage roles.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("Customer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }
        }
    }
}

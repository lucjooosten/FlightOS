using FlightOS.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace FlightOS.Api.Seeding
{
    /// <summary>
    /// Provides methods to seed users and roles in the application.
    /// </summary>
    public class UserInitializer
    {
        /// <summary>
        /// Seeds the admin and customer users along with their roles.
        /// </summary>
        /// <param name="userManager">The user manager to manage user operations.</param>
        /// <param name="roleManager">The role manager to manage role operations.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Check if the admin user exist
            if (await userManager.FindByEmailAsync("admin@flightos.com") == null)
            {
                // Configure the admin user
                var admin = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@flightos.com",
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "User"
                };

                // Create the admin user
                await userManager.CreateAsync(admin, "Admin123!");

                // Check if the admin role exists
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    // Create the admin role
                    await roleManager.CreateAsync(new IdentityRole("Admin"));

                    // Assign the admin role to the admin user
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
                else
                {
                    // Assign the admin role to the admin user
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            // Check if the customer user exist
            if (await userManager.FindByEmailAsync("customer@flightos.com") == null)
            {
                // Configure the admin user
                var customer = new ApplicationUser
                {
                    UserName = "Customer",
                    Email = "customer@flightos.com",
                    EmailConfirmed = true,
                    FirstName = "Customer",
                    LastName = "User"
                };

                // Create the admin user
                await userManager.CreateAsync(customer, "Customer123!");

                // Check if the admin role exists
                if (!await roleManager.RoleExistsAsync("Customer"))
                {
                    // Create the admin role
                    await roleManager.CreateAsync(new IdentityRole("Customer"));

                    // Assign the admin role to the admin user
                    await userManager.AddToRoleAsync(customer, "Customer");
                }
                else
                {
                    // Assign the admin role to the admin user
                    await userManager.AddToRoleAsync(customer, "Customer");
                }
            }
        }
    }
}

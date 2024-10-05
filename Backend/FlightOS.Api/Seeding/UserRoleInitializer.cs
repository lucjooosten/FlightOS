using FlightOS.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace FlightOS.Api.Seeding
{
    /// <summary>
    /// Initializes user roles and users in the system.
    /// </summary>
    public class UserRoleInitializer : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleInitializer"/> class.
        /// </summary>
        /// <param name="scopeFactory">The service scope factory.</param>
        public UserRoleInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        /// <summary>
        /// Starts the asynchronous initialization of user roles and users.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            try
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                await RoleInitializer.SeedRoles(roleManager);
                await UserInitializer.SeedUsers(userManager, roleManager);
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<UserRoleInitializer>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        /// <summary>
        /// Stops the asynchronous operation.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

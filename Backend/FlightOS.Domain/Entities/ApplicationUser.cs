using Microsoft.AspNetCore.Identity;

namespace FlightOS.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Add any additional properties related to the domain
    }
}

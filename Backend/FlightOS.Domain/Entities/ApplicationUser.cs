using Microsoft.AspNetCore.Identity;

namespace FlightOS.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address of the user.
        /// </summary>
        public string? Address { get; set; } = null;

        /// <summary>
        /// Gets or sets the city of the user.
        /// </summary>
        public string? City { get; set; } = null;

        /// <summary>
        /// Gets or sets the state of the user.
        /// </summary>
        public string? State { get; set; } = null;

        /// <summary>
        /// Gets or sets the zip code of the user.
        /// </summary>
        public string? ZipCode { get; set; } = null;
    }
}

namespace FlightOS.Application.DTOs
{
    public class RemoveRoleDto
    {
        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the role to remove from the user.
        /// </summary>
        public string Role { get; set; } = string.Empty;
    }
}

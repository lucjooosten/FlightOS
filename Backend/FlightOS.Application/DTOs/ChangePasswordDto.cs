namespace FlightOS.Application.DTOs
{
    public class ChangePasswordDto
    {
        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the token for resetting the password.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the current password for the user.
        /// </summary>
        public string CurrentPassword { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the new password for the user.
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;
    }
}

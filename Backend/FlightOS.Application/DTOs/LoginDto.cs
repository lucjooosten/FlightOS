﻿namespace FlightOS.Application.DTOs
{
    public class LoginDto
    {
        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for the user.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}

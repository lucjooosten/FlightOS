using FlightOS.Application.DTOs;
using FlightOS.Application.Interfaces;
using FlightOS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace FlightOS.Api.Controllers
{
    /// <summary>
    /// Controller for managing user authentication.
    /// </summary>
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenService _jwtTokenHelper;
        private readonly IEmailService _emailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="jwtTokenHelper">The JWT token helper.</param>
        /// <param name="emailService">The email service.</param>
        public AuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IJwtTokenService jwtTokenHelper, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenHelper = jwtTokenHelper;
            _emailService = emailService;
        }

        /// <summary>
        /// Registers a new user with the specified details.
        /// </summary>
        /// <param name="model">The registration details.</param>
        /// <returns>An IActionResult indicating the result of the registration.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            // Assign the role (e.g., Customer by default)
            var role = model.Role ?? "Customer";
            if (!await _roleManager.RoleExistsAsync(role)) return BadRequest("Role does not exist.");
            await _userManager.AddToRoleAsync(user, role);

            return Ok("User registered successfully!");
        }

        /// <summary>
        /// Logs in a user with the specified credentials.
        /// </summary>
        /// <param name="model">The login details.</param>
        /// <returns>An IActionResult indicating the result of the login attempt.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized();

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenHelper.GenerateJwtToken(user, roles);

            return Ok(new { token });
        }

        /// <summary>  
        /// Changes the password for a user with the specified details.  
        /// </summary>  
        /// <param name="model">The change password details.</param>  
        /// <returns>An IActionResult indicating the result of the password change operation.</returns>  
        [HttpPost("change-password")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return NotFound("User not found.");

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok("Password changed successfully.");
        }


        /// <summary>
        /// Sends a password reset email to the user with the specified email.
        /// </summary>
        /// <param name="model">The forgot password details.</param>
        /// <returns>An IActionResult indicating the result of the forgot password operation.</returns>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return NotFound("User not found.");

            // Generate password reset token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var resetLink = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);
            var resetLink = $"https://flight-os.com/reset-password?token={HttpUtility.UrlEncode(token)}&email={model.Email}";

            // Use the IEmailService to send the email
            var subject = "Reset your password";
            var messageBody = $@"
                                <p>Hi {user.FirstName},</p>
                                <p>You requested a password reset. Click the link below to reset your password:</p>
                                <p><a href='{resetLink}' target='_blank'>Reset Password</a></p>
                                <p>If you did not request this, please ignore this email.</p>
                                <p>Thanks,<br/>FlightOS Team</p>";
            await _emailService.SendEmailAsync(user.Email!, subject, messageBody);

            return Ok("Password reset email sent.");
        }

        /// <summary>  
        /// Resets the password for a user with the specified details.  
        /// </summary>  
        /// <param name="model">The reset password details.</param>  
        /// <returns>An IActionResult indicating the result of the password reset operation.</returns>  
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return NotFound("User not found.");

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (result.Succeeded) return Ok("Password reset successfully.");

            return BadRequest(result.Errors);
        }
    }
}

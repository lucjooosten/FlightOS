using FlightOS.Application.DTOs;
using FlightOS.Application.Interfaces;
using FlightOS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlightOS.Api.Controllers
{
    /// <summary>
    /// Controller for managing user accounts.
    /// </summary>
    [ApiController]
    [Route("api/v1/account")]
    public partial class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenService _jwtTokenHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="jwtTokenHelper">The JWT token helper.</param>
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenService jwtTokenHelper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenHelper = jwtTokenHelper;
        }

        /// <summary>
        /// Retrieves the profile of the currently logged-in user.
        /// </summary>
        /// <returns>An IActionResult containing the user's profile information.</returns>
        [HttpGet("user-profile")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetUserProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound("User not found.");

            var address = new
            {
                user.Address,
                user.City,
                user.State,
                user.ZipCode
            };

            return Ok(new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                user.PhoneNumber,
                address
            });
        }

        /// <summary>
        /// Updates the details of an existing user.
        /// </summary>
        /// <param name="model">The user details to update.</param>
        /// <returns>An IActionResult indicating the result of the update operation.</returns>
        [HttpPut("update-user")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return NotFound("User not found.");

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;
            user.City = model.City;
            user.State = model.State;
            user.ZipCode = model.ZipCode;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return Ok("User updated successfully.");

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Deletes a user with the specified email.
        /// </summary>
        /// <param name="email">The email of the user to delete.</param>
        /// <returns>An IActionResult indicating the result of the delete operation.</returns>
        [HttpDelete("delete-user/{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound("User not found.");

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return Ok("User deleted successfully.");

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        [HttpGet("all-users")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);
        }
    }
}

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
    [Route("api/[controller]")]
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
        /// Creates a new role with the specified name.
        /// </summary>
        /// <param name="roleName">The name of the role to create.</param>
        /// <returns>An IActionResult indicating the result of the role creation.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
                return BadRequest("Role already exists.");

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
                return Ok("Role created successfully!");

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Assigns a role to a user.
        /// </summary>
        /// <param name="model">The model containing the user's email and the role to assign.</param>
        /// <returns>An IActionResult indicating the result of the role assignment.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return NotFound("User not found.");

            if (!await _roleManager.RoleExistsAsync(model.Role))
                return BadRequest("Role does not exist.");

            var result = await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
                return Ok("Role assigned successfully.");

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("all-users")]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);
        }

        /// <summary>
        /// Updates the details of an existing user.
        /// </summary>
        /// <param name="model">The user details to update.</param>
        /// <returns>An IActionResult indicating the result of the update operation.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return NotFound("User not found.");

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
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
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-user/{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound("User not found.");

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return Ok("User deleted successfully.");

            return BadRequest(result.Errors);
        }
    }
}

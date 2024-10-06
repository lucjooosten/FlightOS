using FlightOS.Application.DTOs;
using FlightOS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlightOS.Api.Controllers
{
    /// <summary>
    /// Controller for managing user roles.
    /// </summary>
    [ApiController]
    [Route("api/v1/role")]
    public class RoleController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Creates a new role with the specified name.
        /// </summary>
        /// <param name="roleName">The name of the role to create.</param>
        /// <returns>An IActionResult indicating the result of the role creation.</returns>
        [HttpPost("create-role")]
        [Authorize(Roles = "Admin")]
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
        [HttpPost("assign-role")]
        [Authorize(Roles = "Admin")]
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
        /// Removes a role from a user.
        /// </summary>
        /// <param name="model">The model containing the user's email and the role to remove.</param>
        /// <returns>An IActionResult indicating the result of the role removal.</returns>
        [HttpPost("remove-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRoleFromUser([FromBody] RemoveRoleDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return NotFound("User not found.");

            var result = await _userManager.RemoveFromRoleAsync(user, model.Role);
            if (result.Succeeded) return Ok("Role removed successfully.");

            return BadRequest(result.Errors);
        }
    }
}

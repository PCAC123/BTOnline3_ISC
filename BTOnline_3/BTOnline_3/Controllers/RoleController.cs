using BTOnline_3.IRepository;
using BTOnline_3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTOnline_3.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRepoRole _roleService;
        public RoleController(IRepoRole roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRoleAsync();
            return Ok(roles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            try
            {
                var role = await _roleService.GetByIdAsync(id);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("AllRoleId")]
        public async Task<IActionResult> GetAllRoleId()
        {
            try
            {
                var roleId = await _roleService.GetAllRoleIdsAsync();
                return Ok(roleId);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleModel role)
        {
            if (role == null)
            {
                return BadRequest("Role cannot be null.");
            }
            var createdRole = await _roleService.CreateRoleAsync(role);
            return CreatedAtAction(nameof(GetRoleById), new { id = createdRole.RoleId }, createdRole);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleModel role)
        {
            if (role == null || role.RoleId != id)
            {
                return BadRequest("Role data is invalid.");
            }
            try
            {
                var updatedRole = await _roleService.UpdateRoleAsync(role);
                return Ok(updatedRole);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            if (result)
            {
                return NoContent(); // 204 No Content
            }
            return NotFound($"Role with ID {id} not found.");
        }
    }
}

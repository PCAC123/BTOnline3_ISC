using BTOnline_3.IRepository;
using BTOnline_3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTOnline_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllowAccessController : ControllerBase
    {
        private readonly IRepoAllowAccess _allowAccessService;
        public AllowAccessController(IRepoAllowAccess allowAccessService)
        {
            _allowAccessService = allowAccessService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAllowAccesses()
        {
            var allowAccesses = await _allowAccessService.GetAllAllowAccessesAsync();
            return Ok(allowAccesses);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllowAccessById(int id)
        {
            try
            {
                var allowAccess = await _allowAccessService.GetAllowAccessByIdAsync(id);
                return Ok(allowAccess);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateAllowAccess([FromBody] AllowAccessModel allowAccess)
        {
            if (allowAccess == null) return BadRequest("Allow Access cannot be null.");
            var createdAllowAccess = await _allowAccessService.CreateAllowAccessAsync(allowAccess);
            return CreatedAtAction(nameof(GetAllowAccessById), new { id = createdAllowAccess.AccessId }, createdAllowAccess);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAllowAccess(int id, [FromBody] AllowAccessModel allowAccess)
        {
            if (allowAccess == null || allowAccess.AccessId != id) return BadRequest("Invalid Allow Access data.");
            try
            {
                var updatedAllowAccess = await _allowAccessService.UpdateAllowAccessAsync(allowAccess);
                return Ok(updatedAllowAccess);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAllowAccess(int id)
        {
            var result = await _allowAccessService.DeleteAllowAccessAsync(id);
            if (!result) return NotFound($"Allow Access with ID {id} not found.");
            return NoContent();
        }
    }
}

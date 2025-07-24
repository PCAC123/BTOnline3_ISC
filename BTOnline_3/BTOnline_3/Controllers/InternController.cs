using BTOnline_3.IRepository;
using BTOnline_3.Models;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace BTOnline_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternController : ControllerBase
    {
        private readonly IRepoIntern _internService;
        private readonly IRepoAllowAccess _allowAccessRepo;
        public InternController(IRepoIntern internService, IRepoAllowAccess repoAllowAccess)
        {
            _internService = internService;
            _allowAccessRepo = repoAllowAccess;
        }
        [Authorize]
        [HttpGet("interns")]
        public async Task<IActionResult> GetInterns()
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (roleClaim == null) return Forbid("User role not found.");
            var roleId = int.Parse(roleClaim.Value);

            var access = await _allowAccessRepo.GetAccessAsync(roleId, "Intern");
            if (access == null)
                return Forbid($"Role does not have access to Intern table.");

            var allowedProps = access.AccessProperties
                                      .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                      .Select(p => p.Trim())
                                      .ToHashSet();

            var interns = await _internService.GetFilteredInternsAsync(allowedProps);

            return Ok(interns);
        }
        [HttpGet("interns1")]
        public async Task<IActionResult> GetInterns1()
        {
            var roleId = int.Parse(User.FindFirst(ClaimTypes.Role)?.Value ?? "0");

            var access = await _allowAccessRepo.GetAccessAsync(roleId, "Intern");
            if (access == null)
                return Forbid($"Role does not have access to Intern table.");

            var allowedProps = access.AccessProperties
                                      .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                      .Select(p => p.Trim())
                                      .ToHashSet();

          

            return Ok(allowedProps);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllInterns()
        {
            var interns = await _internService.GetAllInternsAsync();
            return Ok(interns);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInternById(int id)
        {
            try
            {
                var intern = await _internService.GetInternByIdAsync(id);
                return Ok(intern);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateIntern([FromBody] InternModel intern)
        {
            if (intern == null)
                return BadRequest("Invalid intern.");

            // Nếu ImageData vẫn đang là base64 string (user nhập kiểu sai)
            if (intern.ImageData?.Length == 1 && intern.ImageData[0] < 32)
            {
                // Trường hợp sai thường gặp khi input là chuỗi base64 thay vì mảng byte
                return BadRequest("ImageData phải là chuỗi base64 được mã hóa đúng hoặc null.");
            }

            var created = await _internService.CreateInternAsync(intern);
            return CreatedAtAction(nameof(GetInternById), new { id = intern.Id }, created);
        }

    }
}

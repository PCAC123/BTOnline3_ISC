using BTOnline_3.IRepository;
using BTOnline_3.Models;
using BTOnline_3.Service;
using Microsoft.AspNetCore.Mvc;

namespace BTOnline_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepoUser _userService;
        private readonly JwtService _jwtService;

        public AuthController(IRepoUser userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return BadRequest("Email and password are required.");

            var user = await _userService.AuthenticateUserAsync(email, password);
            if (user == null)
                return Unauthorized("Invalid email or password.");

            var token = _jwtService.GenerateToken(user.UserId, user.Email ?? "", (int)user!.RoleId!);

            return Ok(new
            {
                Token = token,
                user.UserId,
                user.Email,
                user.FullName,
                user.RoleId
            });
        }
    }
}

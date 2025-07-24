namespace BTOnline_3.Service
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using Microsoft.IdentityModel.Tokens;
    using DotNetEnv;
    using System.Text;

    public class JwtService
    {
        public string GenerateToken(int userId, string email, int roleId)
        {
            var claims = new[]
            {
            new Claim("userId", userId.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, roleId.ToString()),           
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("JWT_SECRET_KEY")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Env.GetString("JWT_ISSUER"),
                audience: Env.GetString("JWT_AUDIENCE"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(Env.GetString("JWT_EXPIRES_MINUTES")??"60")),
                signingCredentials: creds
            );
            var abc = "Bearer " + new JwtSecurityTokenHandler().WriteToken(token);
            return abc;
        }
    }
}

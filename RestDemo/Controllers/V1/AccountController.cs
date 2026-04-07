using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestDemo.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestDemo.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public AccountController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult Login([FromQuery] string userName, [FromQuery] string pass)
        {
            var token = string.Empty;
            var user = _appDbContext.Users.FirstOrDefault(u => u.Name == userName && u.Password == pass);
            if (user == null)
            {
                return Unauthorized();
            }
            else
            {
                token = GenerateJwtToken(userName, "Admin");
            }

            return Ok(token);
        }

        private string GenerateJwtToken(string username, string role)
        {
            var claims = new[]{
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role)};

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey_123456789012345678901234"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
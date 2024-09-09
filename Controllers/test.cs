using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public TestController(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel userLogin)
        {
            if (userLogin == null)
            {
                return BadRequest("Invalid client request.");
            }

            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = GenerateToken(user);
                return Ok(new { Token = token, user });
            }

            return Unauthorized("User not found or invalid credentials.");
        }

        private string GenerateToken(User user)
        {
            var key = _config["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("JWT key is not configured.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Matricule.ToString()), // Utilisation de ClaimTypes.NameIdentifier pour l'identifiant utilisateur
                new Claim(ClaimTypes.Role, user.Role) // Utilisation de ClaimTypes.Role pour les rôles
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1), // Expiration du token fixée à 1 heure
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(LoginModel userLogin)
        {
            return _context.Users
                .FirstOrDefault(x => x.Matricule == userLogin.Matricule && x.Password == userLogin.Password);
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminEndPoint()
        {
            var currentUser = GetCurrentUser();
            if (currentUser == null)
            {
                return Unauthorized("User not authorized.");
            }
            return Ok($"Hi, you are an {currentUser.Role}");
        }

        private User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims.ToList();
                var matriculeClaim = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var roleClaim = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

                // Ensure matriculeClaim is not null before parsing
                if (int.TryParse(matriculeClaim, out int matricule))
                {
                    return new User
                    {
                        Matricule = matricule,
                        Role = roleClaim
                    };
                }
            }
            return null;
        }
    }
}
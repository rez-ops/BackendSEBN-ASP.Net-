using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebApplication1.Models;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [Authorize]
    [HttpGet("GetAllUsers")]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        try
        {
            var userList = await _context.Users.ToListAsync();
            return Ok(userList);
        }
        catch (Exception ex)
        {
            // Log.Error("Cannot get all users"); // Replace with proper logging
            return StatusCode(500, ex.Message);
        }
    }
[HttpPost("Authenticate")]
    public async Task<ActionResult<User>> Authenticate([FromBody] LoginModel userObj)
    {
        if (userObj == null)
            return BadRequest("Invalid client request");

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Matricule == userObj.Matricule);
        if (user == null)
            return NotFound(new { Message = "User Not Found!" });

        //if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
          //  return BadRequest(new { Message = "Password is incorrect" });

        user.Token = PasswordHasher.CreateJwt(user);
        return Ok(user);
    }
    }
}

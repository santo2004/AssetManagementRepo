using AssetManagement.DTOs;
using AssetManagement.Helpers;
using AssetManagement.Models;
using AssetManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public LoginController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] LoginRequestDto dto)
        {
            var user = _context.Users
                .Include(u => u.Role) 
                .FirstOrDefault(u => u.Username == dto.Username && u.PasswordHash == dto.Password);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            var token = _jwtService.GenerateToken(user.Username, user.Role.RoleName);
            return Ok(token);
        }
    }
}

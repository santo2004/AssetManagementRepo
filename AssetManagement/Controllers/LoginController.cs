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
        public IActionResult Login([FromBody] LoginRequestDto dto)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Username == dto.Username && u.PasswordHash == dto.Password);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            var token = _jwtService.GenerateToken(user.Username, user.Role.RoleName);

            return Ok(new
            {
                token = token,
                user = new
                {
                    user.UserId,
                    user.Username,
                    user.Email,
                    Role = user.Role.RoleName
                }
            });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
                return NotFound("Email not registered.");

            var token = Guid.NewGuid().ToString().Substring(0, 6); 
            user.ResetToken = token;
            user.ResetTokenExpiry = DateTime.UtcNow.AddMinutes(10);
            await _context.SaveChangesAsync();

            Console.WriteLine($"[TOKEN for {dto.Email}]: {token}");

            return Ok(new { token = token });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || user.ResetToken != dto.Token || user.ResetTokenExpiry < DateTime.UtcNow)
                return BadRequest("Invalid or expired token.");

            user.PasswordHash = dto.NewPassword; 
            user.ResetToken = null;
            user.ResetTokenExpiry = null;

            await _context.SaveChangesAsync();
            return Ok("Password reset successfully.");
        }
    }
}

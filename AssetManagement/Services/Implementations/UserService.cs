using AssetManagement.DTOs;
using AssetManagement.Models;
using AssetManagement.Data;
using AssetManagement.Services.Interfaces;
using System.Linq;
using AssetManagement.DTOs.User;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public List<UserViewDto> GetAllUsers()
        {
            return _context.Users
                .Where(u => !u.IsDeleted)
                .Include(u => u.Role)
                .Select(u => new UserViewDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    PhoneNumber = u.PhoneNumber,
                    RoleName = u.Role.RoleName,
                    Email = u.Email
                })
                .ToList();
        }

        public UserViewDto GetUserById(int userId)
        {
            var user = _context.Users
                .Include(u => u.Role) 
                .FirstOrDefault(u => u.UserId == userId);

            if (user == null) return null;

            return new UserViewDto
            {
                UserId = user.UserId,
                Username = user.Username,
                PhoneNumber = user.PhoneNumber,
                RoleName = user.Role?.RoleName,
                Email = user.Email
            };
        }

        public List<UserViewDto> GetUsersByRoleId(int roleId)
        {
            return _context.Users
                .Where(u => u.RoleId == roleId)
                .Select(u => new UserViewDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    PhoneNumber = u.PhoneNumber,
                    RoleName = u.Role.RoleName,
                    Email = u.Email
                }).ToList();
        }

        public string CreateUser(UserCreateDto userDto)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == userDto.RoleId);
            if (role == null) return "Invalid role.";

            if (_context.Users.Any(u => u.Email == userDto.Email))
                return "Email already exists.";
            if (_context.Users.Any(u => u.Username == userDto.Username))
                return "Username already exists.";

            var user = new User
            {
                //UserId = userDto.UserId,
                Username = userDto.Username,
                FullName = userDto.FullName,
                PasswordHash = userDto.Password,
                RoleId = userDto.RoleId,
                Gender = userDto.Gender,
                PhoneNumber = userDto.PhoneNumber,
                Email = userDto.Email,
                Address = userDto.Address
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return "User created successfully.";
        }

        public string DeleteUserById(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId && !u.IsDeleted);
            if (user == null) return "User not found or already deleted.";

            user.IsDeleted = true;
            _context.SaveChanges();
            return "User soft-deleted.";
        }

    }
}

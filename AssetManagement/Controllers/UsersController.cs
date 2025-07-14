using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AssetManagement.Services.Interfaces;
using AssetManagement.DTOs.User;
using AssetManagement.Data;
using AssetManagement.Models;

namespace AssetManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUser")]
        [Authorize]
        public ActionResult<List<UserViewDto>> GetAllUser()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("GetUserById/{id}")]
        public ActionResult<UserViewDto> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound("User not found.");
            return user;
        }

        [HttpGet("GetUserByRoleId/{roleId}")]
        public ActionResult<List<UserViewDto>> GetUsersByRoleId(int roleId)
        {
            return _userService.GetUsersByRoleId(roleId);
        }

        [HttpPost("CreateUser")]
        [Authorize(Roles = "Admin")] 
        public ActionResult<string> CreateUser(UserCreateDto userDto)
        {
            var result = _userService.CreateUser(userDto);
            return Ok(result);
        }

        [HttpPut("DeleteUser/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> DeleteUser(int id)
        {
            var result = _userService.DeleteUserById(id); // This already sets IsDeleted = true
            return Ok(result);
        }
    }
}

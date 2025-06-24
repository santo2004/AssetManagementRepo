using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AssetManagement.Services.Interfaces;
using AssetManagement.DTOs.User;

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
        public ActionResult<List<UserViewDto>> GetAllUsers()
        {
            return _userService.GetAllUsers();
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

        [HttpDelete("DeleteUser/{id}")]
        [Authorize(Roles = "Admin")] 
        public ActionResult<string> DeleteUser(int id)
        {
            var result = _userService.DeleteUserById(id);
            return Ok(result);
        }
    }
}

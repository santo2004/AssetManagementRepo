using Microsoft.AspNetCore.Mvc;
using AssetManagement.Services.Interfaces;
using AssetManagement.DTOs.Role;

namespace AssetManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("GetAllRole")]
        public ActionResult<List<RoleDto>> GetAllRoles()
        {
            return _roleService.GetAllRoles();
        }

        [HttpGet("GetRoleById{id}")]
        public ActionResult<RoleDto> GetRoleById(int id)
        {
            var role = _roleService.GetRoleById(id);
            if (role == null) return NotFound();
            return role;
        }

        //[HttpPost]
        //public ActionResult<string> CreateRole(RoleDto dto)
        //{
        //    return _roleService.CreateRole(dto);
        //}

        //[HttpPut("{id}")]
        //public ActionResult<string> UpdateRole(int id, RoleDto dto)
        //{
        //    return _roleService.UpdateRoleById(id, dto);
        //}

        //[HttpDelete("{id}")]
        //public ActionResult<string> DeleteRole(int id)
        //{
        //    return _roleService.DeleteRoleById(id);
        //}
    }
}

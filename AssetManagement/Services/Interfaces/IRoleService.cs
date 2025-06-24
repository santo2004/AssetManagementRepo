using AssetManagement.DTOs.Role;
using System.Collections.Generic;

namespace AssetManagement.Services.Interfaces
{
    public interface IRoleService
    {
        List<RoleDto> GetAllRoles();
        RoleDto GetRoleById(int roleId);
        string CreateRole(RoleDto roleDto);
        string UpdateRoleById(int roleId, RoleDto updatedRole);
        string DeleteRoleById(int roleId);
    }
}

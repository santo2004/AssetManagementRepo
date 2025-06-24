using AssetManagement.Data;
using AssetManagement.DTOs.Role;
using AssetManagement.Models;
using AssetManagement.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AssetManagement.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext _context;

        public RoleService(AppDbContext context)
        {
            _context = context;
        }

        public List<RoleDto> GetAllRoles()
        {
            return _context.Roles.Select(r => new RoleDto
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName
            }).ToList();
        }

        public RoleDto GetRoleById(int roleId)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == roleId);
            if (role == null) return null;

            return new RoleDto
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName
            };
        }

        public string CreateRole(RoleDto roleDto)
        {
            if (_context.Roles.Any(r => r.RoleName == roleDto.RoleName))
                return "Role already exists.";

            var role = new Role
            {
                RoleName = roleDto.RoleName
            };

            _context.Roles.Add(role);
            _context.SaveChanges();
            return "Role created successfully.";
        }

        public string UpdateRoleById(int roleId, RoleDto updatedRole)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == roleId);
            if (role == null) return "Role not found.";

            role.RoleName = updatedRole.RoleName;
            _context.SaveChanges();
            return "Role updated successfully.";
        }

        public string DeleteRoleById(int roleId)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == roleId);
            if (role == null) return "Role not found.";

            _context.Roles.Remove(role);
            _context.SaveChanges();
            return "Role deleted.";
        }
    }
}

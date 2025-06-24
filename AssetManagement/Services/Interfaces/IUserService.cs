using AssetManagement.DTOs;
using AssetManagement.DTOs.User;

namespace AssetManagement.Services.Interfaces
{
    public interface IUserService
    {
        List<UserViewDto> GetAllUsers();
        UserViewDto GetUserById(int userId);
        List<UserViewDto> GetUsersByRoleId(int roleId);
        string CreateUser(UserCreateDto userDto);
        string DeleteUserById(int userId);
    }
}

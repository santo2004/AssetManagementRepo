namespace AssetManagement.DTOs.User
{
    public class UserViewDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

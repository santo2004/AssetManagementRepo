namespace AssetManagement.DTOs.User
{
    public class UserCreateDto
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }     
        public int RoleId { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}

namespace AssetManagement.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; }
        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //public bool IsActive { get; set; } = true;
        public Role Role { get; set; }
        public ICollection<EmployeeAsset> EmployeeAssets { get; set; } = new List<EmployeeAsset>();
        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
        public ICollection<AuditRequest> AuditRequests { get; set; } = new List<AuditRequest>();
    }
}

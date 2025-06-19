using System.Data;

namespace AssetManagement.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? ContactNumber { get; set; }
        public string? Address { get; set; }
        public int? RoleId { get; set; }
        public DateTime? CreatedAt { get; set; }
        //public virtual ICollection<AssetAuditLog> AssetAuditLogs { get; set; } = new List<AssetAuditLog>();
        public virtual ICollection<AuditRequest> AuditRequests { get; set; } = new List<AuditRequest>();
        public virtual ICollection<EmployeeAsset> EmployeeAssets { get; set; } = new List<EmployeeAsset>();
        //public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
        public virtual Role? Role { get; set; }
        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
        //public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}

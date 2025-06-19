namespace AssetManagement.Models
{
    public class AuditRequest
    {
        public int AuditRequestId { get; set; }
        public int? AssetId { get; set; }
        public int? UserId { get; set; }
        public string? AuditStatus { get; set; }
        public DateTime? AuditDate { get; set; }
        public virtual Asset? Asset { get; set; }
        //public virtual ICollection<AssetAuditLog> AssetAuditLogs { get; set; } = new List<AssetAuditLog>();
        public virtual User? User { get; set; }
    }
}

namespace AssetManagement.Models
{
    public class ServiceRequest
    {
        public int RequestId { get; set; }
        public int? UserId { get; set; }
        public int? AssetId { get; set; }
        public string? Description { get; set; }
        public string? IssueType { get; set; }
        public string? Status { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? ResolvedDate { get; set; }
        public virtual Asset? Asset { get; set; }
        public virtual User? User { get; set; }
    }
}

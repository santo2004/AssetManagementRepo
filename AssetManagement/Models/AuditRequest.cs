namespace AssetManagement.Models
{
    public class AuditRequest
    {
        public int AuditRequestId { get; set; }
        public int AssetId { get; set; }
        public int UserId { get; set; }
        public DateOnly RequestDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public string Status { get; set; }     // Pending, Verified, Rejected  
        public string Comments { get; set; }
        public DateOnly? VerifiedDate { get; set; }
        public Asset Asset { get; set; }
        public User User { get; set; }
    }
}

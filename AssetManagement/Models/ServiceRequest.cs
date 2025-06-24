namespace AssetManagement.Models
{
    public class ServiceRequest
    {
        public int ServiceRequestId { get; set; }
        public int AssetId { get; set; }
        public int UserId { get; set; }
        public DateOnly RequestDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public string IssueType { get; set; }  // Malfunction, Repair
        public string Description { get; set; }
        public string Status { get; set; }     // Pending, InProgress, Completed, Rejected
        public DateOnly? ResolvedDate { get; set; }
        public Asset Asset { get; set; }
        public User User { get; set; }
    }
}

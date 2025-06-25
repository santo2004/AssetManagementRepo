namespace AssetManagement.Models
{
    public class ServiceRequest
    {
        public int ServiceRequestId { get; set; }
        public int AssetId { get; set; }
        public int UserId { get; set; }

        public DateOnly RequestDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public string IssueType { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } // Requested, Under Service, Returned, Rejected
        public DateOnly? ResolvedDate { get; set; }

        public Asset Asset { get; set; }
        public User User { get; set; }
    }
}

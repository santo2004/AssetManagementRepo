namespace AssetManagement.Models
{
    public class AssetRequest
    {
        public int AssetRequestId { get; set; }
        public int AssetId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; } = "Requested"; // Requested, Assigned, Rejected
        public DateOnly RequestDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public DateOnly? ResponseDate { get; set; }

        public Asset Asset { get; set; }
        public User User { get; set; }
    }
}

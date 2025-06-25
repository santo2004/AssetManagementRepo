namespace AssetManagement.DTOs
{
    public class ServiceRequestDto
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public int AssetId { get; set; }
        public string Description { get; set; }
        public string IssueType { get; set; }
        public string Status { get; set; }
        public DateOnly RequestedDate { get; set; }
        public DateOnly? ResolvedDate { get; set; }
    }
}

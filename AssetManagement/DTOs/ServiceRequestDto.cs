public class ServiceRequestDto
{
    public int RequestId { get; set; }
    public int UserId { get; set; }
    public int AssetId { get; set; }
    public string Description { get; set; }
    public string IssueType { get; set; }
    public string Status { get; set; } = "Under Service";
    public DateOnly RequestedDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public DateOnly? ResolvedDate { get; set; }
}

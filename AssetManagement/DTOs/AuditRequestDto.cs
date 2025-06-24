namespace AssetManagement.DTOs
{
    public class AuditRequestDto
    {
        public int AuditRequestId { get; set; }
        public int UserId { get; set; }
        public int AssetId { get; set; }
        public string AuditStatus { get; set; }  
        public string Comments { get; set; }
        public DateOnly AuditDate { get; set; }
    }
}

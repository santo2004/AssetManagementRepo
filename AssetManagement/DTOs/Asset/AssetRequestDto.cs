namespace AssetManagement.DTOs
{
    public class AssetRequestDto
    {
        public int AssetRequestId { get; set; }
        public int AssetId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public DateOnly RequestDate { get; set; }
        public DateOnly? ResponseDate { get; set; }
    }
}

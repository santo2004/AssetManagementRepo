namespace AssetManagement.DTOs.Asset
{
    public class AssetDto
    {
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; } // Include image URL in the DTO
    }
}

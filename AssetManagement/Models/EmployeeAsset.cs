namespace AssetManagement.Models
{
    public class EmployeeAsset
    {
        public int EmployeeAssetId { get; set; }
        public int UserId { get; set; }
        public int AssetId { get; set; }
        public DateOnly AssignedDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
        public string Status { get; set; } 
        public User User { get; set; }
        public Asset Asset { get; set; }
    }
}

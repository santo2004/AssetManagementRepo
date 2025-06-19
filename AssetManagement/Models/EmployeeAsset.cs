namespace AssetManagement.Models
{
    public class EmployeeAsset
    {
        public int AllocationId { get; set; }
        public int? UserId { get; set; }
        public int? AssetId { get; set; }
        public DateTime? AllocationDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? Status { get; set; }
        public virtual Asset? Asset { get; set; }
        public virtual User? User { get; set; }
    }
}

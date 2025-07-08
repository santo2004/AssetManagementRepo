namespace AssetManagement.DTOs
{
    public class EmployeeAssetDto
    {
        public int AllocationId { get; set; }
        public int UserId { get; set; }
        public int AssetId { get; set; }
        public DateOnly AssignedDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
        public string Status { get; set; }
        //public DateOnly AllocationDate { get; internal set; }
    }
}

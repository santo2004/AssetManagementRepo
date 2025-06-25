namespace AssetManagement.Models
{
    public class Asset
    {
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public string Status { get; set; } // Available, Allocated, OutOfStock
        public int Quantity { get; set; }  // New column for quantity

        public ICollection<EmployeeAsset> EmployeeAssets { get; set; } = new List<EmployeeAsset>();
        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
        public ICollection<AuditRequest> AuditRequests { get; set; } = new List<AuditRequest>();
        public ICollection<AssetRequest> AssetRequests { get; set; } = new List<AssetRequest>();
    }
}

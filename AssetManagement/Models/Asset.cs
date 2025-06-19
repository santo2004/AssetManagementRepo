namespace AssetManagement.Models
{
    public class Asset
    {
        public int AssetId { get; set; }
        public string AssetNo { get; set; } = null!;
        public string AssetName { get; set; } = null!;
        public int? CategoryId { get; set; }
        public string? AssetModel { get; set; }
        public DateOnly? ManufacturingDate { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        public decimal? AssetValue { get; set; }
        public string? Status { get; set; }
        public virtual ICollection<AuditRequest> AuditRequests { get; set; } = new List<AuditRequest>();
        //public virtual AssetCategory? Category { get; set; }
        public virtual ICollection<EmployeeAsset> EmployeeAssets { get; set; } = new List<EmployeeAsset>();
        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
    }
}

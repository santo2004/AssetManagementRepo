using AssetManagement.Data;
using AssetManagement.DTOs;
using AssetManagement.Models;
using AssetManagement.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AssetManagement.Services.Implementations
{
    public class AssetService : IAssetService
    {
        private readonly AppDbContext _context;

        public AssetService(AppDbContext context)
        {
            _context = context;
        }

        public List<AssetDto> GetAllAssets()
        {
            return _context.Assets.Select(a => new AssetDto
            {
                AssetId = a.AssetId,
                AssetName = a.AssetName,
                Status = a.Status
            }).ToList();
        }

        public AssetDto GetAssetById(int assetId)
        {
            var asset = _context.Assets.FirstOrDefault(a => a.AssetId == assetId);
            if (asset == null) return null;

            return new AssetDto
            {
                AssetId = asset.AssetId,
                AssetName = asset.AssetName,
                Status = asset.Status
            };
        }

        public List<AssetDto> GetAssetsByStatus(string status)
        {
            return _context.Assets
                .Where(a => a.Status.ToLower() == status.ToLower())
                .Select(a => new AssetDto
                {
                    AssetId = a.AssetId,
                    AssetName = a.AssetName,
                    Status = a.Status
                }).ToList();
        }

        public string CreateAsset(AssetDto dto)
        {
            var asset = new Asset
            {
                AssetName = dto.AssetName,
                Status = dto.Status
            };

            _context.Assets.Add(asset);
            _context.SaveChanges();
            return "Asset created successfully.";
        }

        public string UpdateAssetById(int assetId, AssetDto dto)
        {
            var asset = _context.Assets.FirstOrDefault(a => a.AssetId == assetId);
            if (asset == null) return "Asset not found.";

            asset.AssetName = dto.AssetName;
            asset.Status = dto.Status;

            _context.SaveChanges();
            return "Asset updated successfully.";
        }

        public string DeleteAssetById(int assetId)
        {
            var asset = _context.Assets.FirstOrDefault(a => a.AssetId == assetId);
            if (asset == null) return "Asset not found.";

            _context.Assets.Remove(asset);
            _context.SaveChanges();
            return "Asset deleted.";
        }

        public string RequestAsset(int assetId, int userId)
        {
            var asset = _context.Assets.FirstOrDefault(a => a.AssetId == assetId);
            if (asset == null) return "Asset not found.";

            if (asset.Status != "Available") return "Asset is not available.";

            asset.Status = "Requested";
            _context.SaveChanges();

            return "Asset request submitted.";
        }

        public List<AssetDto> GetRequestedAssets()
        {
            return _context.Assets
                .Where(a => a.Status == "Requested")
                .Select(a => new AssetDto
                {
                    AssetId = a.AssetId,
                    AssetName = a.AssetName,
                    Status = a.Status
                }).ToList();
        }

        public string AssignAssetToUser(int assetId, int userId)
        {
            var asset = _context.Assets.FirstOrDefault(a => a.AssetId == assetId);
            if (asset == null) return "Asset not found.";

            if (asset.Status != "Requested") return "Asset was not requested.";

            asset.Status = "Allocated";

            var employeeAsset = new EmployeeAsset
            {
                AssetId = assetId,
                UserId = userId,
                AssignedDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Status = "Allocated"
            };

            _context.EmployeeAssets.Add(employeeAsset);
            _context.SaveChanges();

            return "Asset assigned to user.";
        }

        public string RejectAssetRequest(int assetId, int userId, string comments)
        {
            var asset = _context.Assets.FirstOrDefault(a => a.AssetId == assetId);
            if (asset == null) return "Asset not found.";

            if (asset.Status != "Requested") return "Asset was not requested.";

            // Reset the asset status
            asset.Status = "Available";

            // Log in audit request
            var audit = new AuditRequest
            {
                AssetId = assetId,
                UserId = userId,
                Comments = comments,
                Status = "Rejected",
                VerifiedDate = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            _context.AuditRequests.Add(audit);
            _context.SaveChanges();

            return "Asset request rejected and logged.";
        }

        private readonly IAuditRequestService _auditService;
        private readonly IEmployeeAssetService _employeeAssetService;

        public AssetService(AppDbContext context, IAuditRequestService auditService, IEmployeeAssetService employeeAssetService)
        {
            _context = context;
            _auditService = auditService;
            _employeeAssetService = employeeAssetService;
        }
    }
}

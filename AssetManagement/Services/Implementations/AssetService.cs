using AssetManagement.Data;
using AssetManagement.DTOs;
using AssetManagement.DTOs.Asset;
using AssetManagement.Models;
using AssetManagement.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AssetManagement.Services.Implementations
{
    public class AssetService : IAssetService
    {
        private readonly AppDbContext _context;
        private readonly IAuditRequestService _auditService;
        private readonly IEmployeeAssetService _employeeAssetService;

        public AssetService(AppDbContext context, IAuditRequestService auditService, IEmployeeAssetService employeeAssetService)
        {
            _context = context;
            _auditService = auditService;
            _employeeAssetService = employeeAssetService;
        }

        public List<AssetDto> GetAllAssets()
        {
            return _context.Assets.Select(a => new AssetDto
            {
                AssetId = a.AssetId,
                AssetName = a.AssetName,
                Status = a.Status,
                Quantity = a.Quantity,
                ImageUrl = a.ImageUrl,
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
                Status = asset.Status,
                Quantity = asset.Quantity,
                ImageUrl = asset.ImageUrl,
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
                    Status = a.Status,
                    Quantity = a.Quantity,
                    ImageUrl = a.ImageUrl
                }).ToList();
        }

        public string CreateAsset(AssetDto dto)
        {
            var asset = new Asset
            {
                AssetName = dto.AssetName,
                Status = dto.Status,
                Quantity = dto.Quantity,
                ImageUrl = dto.ImageUrl,
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
            asset.Quantity = dto.Quantity;
            asset.ImageUrl = dto.ImageUrl;

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

            var assetReq = new AssetRequest
            {
                AssetId = assetId,
                UserId = userId,
                Status = "Requested"
            };
            _context.AssetRequests.Add(assetReq);

            var audit = new AuditRequest
            {
                AssetId = assetId,
                UserId = userId,
                Status = "Requested",
                RequestDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Comments = "asset requested"
            };
            _context.AuditRequests.Add(audit);

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
                    Status = a.Status,
                    Quantity = a.Quantity
                }).ToList();
        }

        public string AssignAssetToUser(int assetRequestId, int userId)
        {
            var assetRequest = _context.AssetRequests.FirstOrDefault(r => r.AssetRequestId == assetRequestId);
            if (assetRequest == null || assetRequest.Status != "Requested")
                return "Invalid request";

            var asset = _context.Assets.FirstOrDefault(a => a.AssetId == assetRequest.AssetId);
            if (asset == null || asset.Quantity <= 0)
            {
                assetRequest.Status = "Rejected";
                _context.SaveChanges();
                return "Asset not available";
            }

            asset.Quantity -= 1;
            asset.Status = asset.Quantity == 0 ? "Out of Stock" : "Available";
            assetRequest.Status = "Assigned";

            // ✅ Use AutoCreateAudit or CreateAuditRequest correctly
            _auditService.CreateAuditRequest(new AuditRequestDto
            {
                UserId = userId,
                AssetId = asset.AssetId,
                AuditStatus = "Assigned",
                Comments = "Asset assigned via system",
                AuditDate = DateOnly.FromDateTime(DateTime.Now)
            });

            _employeeAssetService.CreateAllocation(new EmployeeAssetDto
            {
                UserId = userId,
                AssetId = asset.AssetId,
                Status = "Allocated",
                AssignedDate = DateOnly.FromDateTime(DateTime.Now)
            });

            _context.SaveChanges();
            return "Asset assigned successfully";
        }

        public string RejectAssetRequest(int assetRequestId, int userId, string comment)
        {
            var request = _context.AssetRequests.FirstOrDefault(r => r.AssetRequestId == assetRequestId);
            if (request == null) return "Request not found.";

            request.Status = "Rejected";

            var audit = new AuditRequest
            {
                AssetId = request.AssetId,
                UserId = request.UserId,
                Status = "Rejected",
                Comments = comment,
                VerifiedDate = DateOnly.FromDateTime(DateTime.UtcNow)
            };
            _context.AuditRequests.Add(audit);

            _context.SaveChanges();
            return "Asset request rejected.";
        }
    }
}

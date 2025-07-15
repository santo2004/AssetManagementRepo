using AssetManagement.Data;
using AssetManagement.DTOs;
using AssetManagement.Models;
using AssetManagement.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AssetManagement.Services.Implementations
{
    public class AssetRequestService : IAssetRequestService
    {
        private readonly AppDbContext _context;

        public AssetRequestService(AppDbContext context)
        {
            _context = context;
        }

        public string CreateAssetRequest(int assetId, int userId)
        {
            var request = new AssetRequest
            {
                AssetId = assetId,
                UserId = userId,
                Status = "Requested",
                RequestDate = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            _context.EmployeeAssets.Add(new EmployeeAsset
            {
                UserId = request.UserId,
                AssetId = request.AssetId,
                AssignedDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Status = "Allocated"
            });

            var asset = _context.Assets.FirstOrDefault(a => a.AssetId == request.AssetId);
            if (asset != null)
            {
                asset.Quantity -= 1;
                asset.Status = asset.Quantity == 0 ? "Out of Stock" : "Available";
            }

            _context.AssetRequests.Add(request);
            _context.SaveChanges();

            return "Asset request submitted.";
        }

        public List<AssetRequestDto> GetAllRequests()
        {
            return _context.AssetRequests.Select(r => new AssetRequestDto
            {
                AssetRequestId = r.AssetRequestId,
                AssetId = r.AssetId,
                UserId = r.UserId,
                Status = r.Status,
                RequestDate = r.RequestDate,
                ResponseDate = r.ResponseDate
            }).ToList();
        }

        public List<AssetRequestDto> GetRequestsByStatus(string status)
        {
            return _context.AssetRequests
                .Where(r => r.Status.ToLower() == status.ToLower())
                .Select(r => new AssetRequestDto
                {
                    AssetRequestId = r.AssetRequestId,
                    AssetId = r.AssetId,
                    UserId = r.UserId,
                    Status = r.Status,
                    RequestDate = r.RequestDate,
                    ResponseDate = r.ResponseDate
                }).ToList();
        }

        public List<AssetRequestDto> GetRequestsByUserId(int userId)
        {
            return _context.AssetRequests
                .Where(r => r.UserId == userId)
                .Select(r => new AssetRequestDto
                {
                    AssetRequestId = r.AssetRequestId,
                    AssetId = r.AssetId,
                    UserId = r.UserId,
                    Status = r.Status,
                    RequestDate = r.RequestDate,
                    ResponseDate = r.ResponseDate
                }).ToList();
        }

        public string ApproveRequest(int requestId)
        {
            var request = _context.AssetRequests.FirstOrDefault(r => r.AssetRequestId == requestId);
            if (request == null) return "Request not found.";

            request.Status = "Assigned";
            request.ResponseDate = DateOnly.FromDateTime(DateTime.UtcNow);

            _context.SaveChanges();
            return "Asset request approved.";
        }

        public string RejectRequest(int requestId, string comment)
        {
            var request = _context.AssetRequests.FirstOrDefault(r => r.AssetRequestId == requestId);
            if (request == null) return "Request not found.";

            request.Status = "Rejected";
            request.ResponseDate = DateOnly.FromDateTime(DateTime.UtcNow);

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

            return "Asset request rejected and logged.";
        }
    }
}

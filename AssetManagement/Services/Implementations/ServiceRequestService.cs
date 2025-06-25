using AssetManagement.Data;
using AssetManagement.DTOs;
using AssetManagement.Models;
using AssetManagement.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AssetManagement.Services.Implementations
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly AppDbContext _context;

        public ServiceRequestService(AppDbContext context)
        {
            _context = context;
        }

        public List<ServiceRequestDto> GetAllRequests()
        {
            return _context.ServiceRequests.Select(r => new ServiceRequestDto
            {
                RequestId = r.ServiceRequestId,
                UserId = r.UserId,
                AssetId = r.AssetId,
                Description = r.Description,
                IssueType = r.IssueType,
                Status = r.Status,
                RequestedDate = r.RequestDate,
                ResolvedDate = r.ResolvedDate
            }).ToList();
        }

        public ServiceRequestDto GetRequestById(int requestId)
        {
            var req = _context.ServiceRequests.FirstOrDefault(r => r.ServiceRequestId == requestId);
            if (req == null) return null;

            return new ServiceRequestDto
            {
                RequestId = req.ServiceRequestId,
                UserId = req.UserId,
                AssetId = req.AssetId,
                Description = req.Description,
                IssueType = req.IssueType,
                Status = req.Status,
                RequestedDate = req.RequestDate,
                ResolvedDate = req.ResolvedDate
            };
        }

        public List<ServiceRequestDto> GetRequestsByUserId(int userId)
        {
            return _context.ServiceRequests
                .Where(r => r.UserId == userId)
                .Select(r => new ServiceRequestDto
                {
                    RequestId = r.ServiceRequestId,
                    UserId = r.UserId,
                    AssetId = r.AssetId,
                    Description = r.Description,
                    IssueType = r.IssueType,
                    Status = r.Status,
                    RequestedDate = r.RequestDate,
                    ResolvedDate = r.ResolvedDate
                }).ToList();
        }

        public string CreateRequest(ServiceRequestDto dto)
        {
            var service = new ServiceRequest
            {
                UserId = dto.UserId,
                AssetId = dto.AssetId,
                Description = dto.Description,
                IssueType = dto.IssueType,
                Status = "Under Service", // default
                RequestDate = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            _context.ServiceRequests.Add(service);
            _context.SaveChanges();
            return "Service request created and marked as Under Service.";
        }

        public string UpdateRequestById(int requestId, ServiceRequestDto dto)
        {
            var req = _context.ServiceRequests.FirstOrDefault(r => r.ServiceRequestId == requestId);
            if (req == null) return "Service request not found.";

            req.Description = dto.Description;
            req.IssueType = dto.IssueType;
            req.Status = dto.Status;
            req.ResolvedDate = dto.ResolvedDate;

            _context.SaveChanges();
            return "Service request updated.";
        }

        public string DeleteRequestById(int requestId)
        {
            var req = _context.ServiceRequests.FirstOrDefault(r => r.ServiceRequestId == requestId);
            if (req == null) return "Service request not found.";

            _context.ServiceRequests.Remove(req);
            _context.SaveChanges();
            return "Service request deleted.";
        }

        public string MarkAsUnderService(int requestId)
        {
            var req = _context.ServiceRequests.FirstOrDefault(r => r.ServiceRequestId == requestId);
            if (req == null) return "Request not found.";

            req.Status = "Under Service";
            _context.SaveChanges();
            return "Service marked as Under Service.";
        }

        public string MarkAsReturned(int requestId)
        {
            var req = _context.ServiceRequests.FirstOrDefault(r => r.ServiceRequestId == requestId);
            if (req == null) return "Request not found.";

            req.Status = "Returned";
            req.ResolvedDate = DateOnly.FromDateTime(DateTime.UtcNow);

            _context.SaveChanges();
            return "Service marked as Returned.";
        }

        public string RejectRequest(int requestId, string reason)
        {
            var req = _context.ServiceRequests.FirstOrDefault(r => r.ServiceRequestId == requestId);
            if (req == null) return "Request not found.";

            req.Status = "Rejected";
            req.ResolvedDate = DateOnly.FromDateTime(DateTime.UtcNow);
            req.Description += $" | Rejected Reason: {reason}";

            _context.SaveChanges();
            return "Service request rejected.";
        }
    }
}

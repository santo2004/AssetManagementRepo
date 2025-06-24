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

        public string CreateRequest(ServiceRequestDto requestDto)
        {
            var req = new ServiceRequest
            {
                UserId = requestDto.UserId,
                AssetId = requestDto.AssetId,
                Description = requestDto.Description,
                IssueType = requestDto.IssueType,
                Status = requestDto.Status,
                RequestDate = requestDto.RequestedDate,
                ResolvedDate = requestDto.ResolvedDate
            };

            _context.ServiceRequests.Add(req);
            _context.SaveChanges();
            return "Service request created.";
        }

        public string UpdateRequestById(int requestId, ServiceRequestDto dto)
        {
            var req = _context.ServiceRequests.FirstOrDefault(r => r.ServiceRequestId == requestId);
            if (req == null) return "Service request not found.";

            req.UserId = dto.UserId;
            req.AssetId = dto.AssetId;
            req.Description = dto.Description;
            req.IssueType = dto.IssueType;
            req.Status = dto.Status;
            req.RequestDate = dto.RequestedDate;
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
    }
}

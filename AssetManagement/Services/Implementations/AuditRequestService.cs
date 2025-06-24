using AssetManagement.Data;
using AssetManagement.DTOs;
using AssetManagement.Models;
using AssetManagement.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AssetManagement.Services.Implementations
{
    public class AuditRequestService : IAuditRequestService
    {
        private readonly AppDbContext _context;

        public AuditRequestService(AppDbContext context)
        {
            _context = context;
        }

        public List<AuditRequestDto> GetAllAuditRequests()
        {
            return _context.AuditRequests.Select(a => new AuditRequestDto
            {
                AuditRequestId = a.AuditRequestId,
                UserId = a.UserId,
                AssetId = a.AssetId,
                AuditStatus = a.Status,
                Comments = a.Comments,
                AuditDate = a.VerifiedDate ?? DateOnly.MinValue
            }).ToList();
        }

        public AuditRequestDto GetAuditRequestById(int auditRequestId)
        {
            var audit = _context.AuditRequests.FirstOrDefault(a => a.AuditRequestId == auditRequestId);
            if (audit == null) return null;

            return new AuditRequestDto
            {
                AuditRequestId = audit.AuditRequestId,
                UserId = audit.UserId,
                AssetId = audit.AssetId,
                AuditStatus = audit.Status,
                Comments = audit.Comments,
                AuditDate = audit.VerifiedDate ?? DateOnly.MinValue
            };
        }

        public List<AuditRequestDto> GetAuditRequestsByUserId(int userId)
        {
            return _context.AuditRequests
                .Where(a => a.UserId == userId)
                .Select(a => new AuditRequestDto
                {
                    AuditRequestId = a.AuditRequestId,
                    UserId = a.UserId,
                    AssetId = a.AssetId,
                    AuditStatus = a.Status,
                    Comments = a.Comments,
                    AuditDate = a.VerifiedDate ?? DateOnly.MinValue
                }).ToList();
        }

        public string CreateAuditRequest(AuditRequestDto auditDto)
        {
            var audit = new AuditRequest
            {
                UserId = auditDto.UserId,
                AssetId = auditDto.AssetId,
                Status = auditDto.AuditStatus,
                Comments = auditDto.Comments,
                VerifiedDate = auditDto.AuditDate
            };

            _context.AuditRequests.Add(audit);
            _context.SaveChanges();
            return "Audit request created.";
        }

        public string UpdateAuditRequestById(int auditRequestId, AuditRequestDto updatedDto)
        {
            var audit = _context.AuditRequests.FirstOrDefault(a => a.AuditRequestId == auditRequestId);
            if (audit == null) return "Audit request not found.";

            audit.UserId = updatedDto.UserId;
            audit.AssetId = updatedDto.AssetId;
            audit.Status = updatedDto.AuditStatus;
            audit.Comments = updatedDto.Comments;
            audit.VerifiedDate = updatedDto.AuditDate;

            _context.SaveChanges();
            return "Audit request updated.";
        }

        public string DeleteAuditRequestById(int auditRequestId)
        {
            var audit = _context.AuditRequests.FirstOrDefault(a => a.AuditRequestId == auditRequestId);
            if (audit == null) return "Audit request not found.";

            _context.AuditRequests.Remove(audit);
            _context.SaveChanges();
            return "Audit request deleted.";
        }
    }
}

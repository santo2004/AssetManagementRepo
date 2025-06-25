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

        public string CreateAuditRequest(AuditRequestDto dto)
        {
            var audit = new AuditRequest
            {
                UserId = dto.UserId,
                AssetId = dto.AssetId,
                Status = dto.AuditStatus,
                Comments = dto.Comments,
                VerifiedDate = dto.AuditDate
            };

            _context.AuditRequests.Add(audit);
            _context.SaveChanges();
            return "Audit request created.";
        }

        public string UpdateAuditRequestById(int id, AuditRequestDto dto)
        {
            var audit = _context.AuditRequests.FirstOrDefault(a => a.AuditRequestId == id);
            if (audit == null) return "Audit request not found.";

            audit.UserId = dto.UserId;
            audit.AssetId = dto.AssetId;
            audit.Status = dto.AuditStatus;
            audit.Comments = dto.Comments;
            audit.VerifiedDate = dto.AuditDate;

            _context.SaveChanges();
            return "Audit request updated.";
        }

        public string DeleteAuditRequestById(int id)
        {
            var audit = _context.AuditRequests.FirstOrDefault(a => a.AuditRequestId == id);
            if (audit == null) return "Audit request not found.";

            _context.AuditRequests.Remove(audit);
            _context.SaveChanges();
            return "Audit request deleted.";
        }

        // ✅ Automatically create an audit (used by AssetService, EmployeeAssetService, etc.)
        public string AutoCreateAudit(int assetId, int userId, string status, string comment = "")
        {
            var audit = new AuditRequest
            {
                AssetId = assetId,
                UserId = userId,
                Status = status, // Requested, Verified, Rejected, Returned
                Comments = comment,
                VerifiedDate = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            _context.AuditRequests.Add(audit);
            _context.SaveChanges();

            return "Audit auto-logged.";
        }

        // ✅ To manually mark audit as "In Audit"
        public string MarkAuditInProgress(int auditRequestId)
        {
            var audit = _context.AuditRequests.FirstOrDefault(a => a.AuditRequestId == auditRequestId);
            if (audit == null) return "Audit not found.";

            audit.Status = "In Audit";
            audit.VerifiedDate = DateOnly.FromDateTime(DateTime.UtcNow);
            _context.SaveChanges();

            return "Audit marked as In Audit.";
        }
    }
}

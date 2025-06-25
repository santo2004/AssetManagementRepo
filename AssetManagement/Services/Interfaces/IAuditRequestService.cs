using AssetManagement.DTOs;
using System.Collections.Generic;

namespace AssetManagement.Services.Interfaces
{
    public interface IAuditRequestService
    {
        List<AuditRequestDto> GetAllAuditRequests();
        AuditRequestDto GetAuditRequestById(int auditRequestId);
        List<AuditRequestDto> GetAuditRequestsByUserId(int userId);
        string CreateAuditRequest(AuditRequestDto auditDto);
        string UpdateAuditRequestById(int auditRequestId, AuditRequestDto updatedDto);
        string DeleteAuditRequestById(int auditRequestId);

        // Optional helper methods
        string AutoCreateAudit(int assetId, int userId, string status, string comment = "");
        string MarkAuditInProgress(int auditRequestId);
    }
}

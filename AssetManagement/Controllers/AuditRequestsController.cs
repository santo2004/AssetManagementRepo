using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AssetManagement.Services.Interfaces;
using AssetManagement.DTOs;

namespace AssetManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AuditRequestsController : ControllerBase
    {
        private readonly IAuditRequestService _auditRequestService;

        public AuditRequestsController(IAuditRequestService auditRequestService)
        {
            _auditRequestService = auditRequestService;
        }

        [HttpGet("GetAllAudit")]
        public ActionResult<List<AuditRequestDto>> GetAllAuditRequests()
        {
            return _auditRequestService.GetAllAuditRequests();
        }

        [HttpGet("GetAuditByAuditId/{id}")]
        public ActionResult<AuditRequestDto> GetAuditRequestById(int id)
        {
            var audit = _auditRequestService.GetAuditRequestById(id);
            if (audit == null) return NotFound("Audit request not found.");
            return audit;
        }

        [HttpGet("GetAuditByUserId/{userId}")]
        public ActionResult<List<AuditRequestDto>> GetAuditRequestsByUserId(int userId)
        {
            return _auditRequestService.GetAuditRequestsByUserId(userId);
        }

        [HttpPost("CreateAudit")]
        public ActionResult<string> CreateAuditRequest(AuditRequestDto dto)
        {
            return _auditRequestService.CreateAuditRequest(dto);
        }

        [HttpPut("UpdateAudit/{id}")]
        public ActionResult<string> UpdateAuditRequest(int id, AuditRequestDto dto)
        {
            return _auditRequestService.UpdateAuditRequestById(id, dto);
        }

        [HttpDelete("DeleteAudit/{id}")]
        public ActionResult<string> DeleteAuditRequest(int id)
        {
            return _auditRequestService.DeleteAuditRequestById(id);
        }
    }
}


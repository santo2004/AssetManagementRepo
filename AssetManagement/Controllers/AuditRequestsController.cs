using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AssetManagement.Services.Interfaces;
using AssetManagement.DTOs;

namespace AssetManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // 🔐 Restrict all endpoints to Admin only
    public class AuditRequestsController : ControllerBase
    {
        private readonly IAuditRequestService _auditRequestService;

        public AuditRequestsController(IAuditRequestService auditRequestService)
        {
            _auditRequestService = auditRequestService;
        }

        [HttpGet]
        public ActionResult<List<AuditRequestDto>> GetAllAuditRequests()
        {
            return _auditRequestService.GetAllAuditRequests();
        }

        [HttpGet("{id}")]
        public ActionResult<AuditRequestDto> GetAuditRequestById(int id)
        {
            var audit = _auditRequestService.GetAuditRequestById(id);
            if (audit == null) return NotFound("Audit request not found.");
            return audit;
        }

        [HttpGet("by-user/{userId}")]
        public ActionResult<List<AuditRequestDto>> GetAuditRequestsByUserId(int userId)
        {
            return _auditRequestService.GetAuditRequestsByUserId(userId);
        }

        [HttpPost]
        public ActionResult<string> CreateAuditRequest(AuditRequestDto dto)
        {
            return _auditRequestService.CreateAuditRequest(dto);
        }

        [HttpPut("{id}")]
        public ActionResult<string> UpdateAuditRequest(int id, AuditRequestDto dto)
        {
            return _auditRequestService.UpdateAuditRequestById(id, dto);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteAuditRequest(int id)
        {
            return _auditRequestService.DeleteAuditRequestById(id);
        }
    }
}


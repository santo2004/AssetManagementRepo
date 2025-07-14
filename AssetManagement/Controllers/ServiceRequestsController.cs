using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AssetManagement.Services.Interfaces;
using AssetManagement.DTOs;
using AssetManagement.Data;
using System.Linq;

namespace AssetManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServiceRequestsController : ControllerBase
    {
        private readonly IServiceRequestService _serviceRequestService;
        private readonly AppDbContext _context;

        public ServiceRequestsController(IServiceRequestService serviceRequestService, AppDbContext context)
        {
            _serviceRequestService = serviceRequestService;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllRequests")]
        public ActionResult<List<ServiceRequestDto>> GetAllRequests()
        {
            return _serviceRequestService.GetAllRequests();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetRequestById/{id}")]
        public ActionResult<ServiceRequestDto> GetRequestById(int id)
        {
            var request = _serviceRequestService.GetRequestById(id);
            if (request == null) return NotFound("Request not found.");
            return request;
        }

        [HttpGet("GetRequestByUserId/{userId}")]
        public ActionResult<List<ServiceRequestDto>> GetRequestsByUserId(int userId)
        {
            return _serviceRequestService.GetRequestsByUserId(userId);
        }

        
        [HttpPost("CreateRequest")]
        public ActionResult<string> CreateRequest([FromBody] ServiceRequestDto dto)
        {
            return _serviceRequestService.CreateRequest(dto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateRequest/{id}")]
        public ActionResult<string> UpdateRequest(int id, [FromBody] ServiceRequestDto dto)
        {
            return _serviceRequestService.UpdateRequestById(id, dto);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var req = _context.ServiceRequests.FirstOrDefault(r => r.ServiceRequestId == id);
            if (req == null) return NotFound("Request not found.");

            _context.ServiceRequests.Remove(req);
            _context.SaveChanges();
            return Ok("Service request deleted.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("MarkUnderService/{id}")]
        public ActionResult<string> MarkUnderService(int id)
        {
            return _serviceRequestService.MarkAsUnderService(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("MarkReturned/{id}")]
        public ActionResult<string> MarkReturned(int id)
        {
            return _serviceRequestService.MarkAsReturned(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("RejectRequest/{id}")]
        public ActionResult<string> RejectRequest(int id, [FromQuery] string reason)
        {
            return _serviceRequestService.RejectRequest(id, reason);
        }
    }
}

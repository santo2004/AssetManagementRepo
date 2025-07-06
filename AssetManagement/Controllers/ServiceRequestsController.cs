using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AssetManagement.Services.Interfaces;
using AssetManagement.DTOs;

namespace AssetManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class ServiceRequestsController : ControllerBase
    {
        private readonly IServiceRequestService _serviceRequestService;

        public ServiceRequestsController(IServiceRequestService serviceRequestService)
        {
            _serviceRequestService = serviceRequestService;
        }

        [HttpGet("GetAllRequest")]
        public ActionResult<List<ServiceRequestDto>> GetAllRequests()
        {
            return _serviceRequestService.GetAllRequests();
        }

        [HttpGet("GetRequestById{id}")]
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

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateRequest")]
        public ActionResult<string> CreateRequest(ServiceRequestDto dto)
        {
            return _serviceRequestService.CreateRequest(dto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateRequest{id}")]
        public ActionResult<string> UpdateRequest(int id, ServiceRequestDto dto)
        {
            return _serviceRequestService.UpdateRequestById(id, dto);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteRequest{id}")]
        public ActionResult<string> DeleteRequest(int id)
        {
            return _serviceRequestService.DeleteRequestById(id);
        }


        [HttpPost("MarkUnderService/{id}")]
        public ActionResult<string> MarkUnderService(int id)
        {
            return _serviceRequestService.MarkAsUnderService(id);
        }

        [HttpPost("MarkReturned/{id}")]
        public ActionResult<string> MarkReturned(int id)
        {
            return _serviceRequestService.MarkAsReturned(id);
        }

        [HttpPost("RejectRequest/{id}")]
        public ActionResult<string> RejectRequest(int id, [FromQuery] string reason)
        {
            return _serviceRequestService.RejectRequest(id, reason);
        }
    }
}

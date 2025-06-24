using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AssetManagement.Services.Interfaces;
using AssetManagement.DTOs;

namespace AssetManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] 
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

        [HttpPost("CreateRequest")]
        public ActionResult<string> CreateRequest(ServiceRequestDto dto)
        {
            return _serviceRequestService.CreateRequest(dto);
        }

        [HttpPut("UpdateRequest{id}")]
        public ActionResult<string> UpdateRequest(int id, ServiceRequestDto dto)
        {
            return _serviceRequestService.UpdateRequestById(id, dto);
        }

        [HttpDelete("DeleteRequest{id}")]
        public ActionResult<string> DeleteRequest(int id)
        {
            return _serviceRequestService.DeleteRequestById(id);
        }
    }
}

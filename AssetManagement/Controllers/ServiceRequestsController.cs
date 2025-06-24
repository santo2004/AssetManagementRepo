using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AssetManagement.Services.Interfaces;
using AssetManagement.DTOs;

namespace AssetManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // All endpoints in this controller are for Admin only
    public class ServiceRequestsController : ControllerBase
    {
        private readonly IServiceRequestService _serviceRequestService;

        public ServiceRequestsController(IServiceRequestService serviceRequestService)
        {
            _serviceRequestService = serviceRequestService;
        }

        [HttpGet]
        public ActionResult<List<ServiceRequestDto>> GetAllRequests()
        {
            return _serviceRequestService.GetAllRequests();
        }

        [HttpGet("{id}")]
        public ActionResult<ServiceRequestDto> GetRequestById(int id)
        {
            var request = _serviceRequestService.GetRequestById(id);
            if (request == null) return NotFound("Request not found.");
            return request;
        }

        [HttpGet("by-user/{userId}")]
        public ActionResult<List<ServiceRequestDto>> GetRequestsByUserId(int userId)
        {
            return _serviceRequestService.GetRequestsByUserId(userId);
        }

        [HttpPost]
        public ActionResult<string> CreateRequest(ServiceRequestDto dto)
        {
            return _serviceRequestService.CreateRequest(dto);
        }

        [HttpPut("{id}")]
        public ActionResult<string> UpdateRequest(int id, ServiceRequestDto dto)
        {
            return _serviceRequestService.UpdateRequestById(id, dto);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteRequest(int id)
        {
            return _serviceRequestService.DeleteRequestById(id);
        }
    }
}

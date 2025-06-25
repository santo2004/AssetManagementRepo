using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AssetManagement.Services.Interfaces;
using AssetManagement.DTOs;

namespace AssetManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AssetRequestsController : ControllerBase
    {
        private readonly IAssetRequestService _assetRequestService;

        public AssetRequestsController(IAssetRequestService assetRequestService)
        {
            _assetRequestService = assetRequestService;
        }

        [HttpPost("Request")]
        [Authorize(Roles = "Employee,Manager")]
        public ActionResult<string> RequestAsset(int assetId, int userId)
        {
            return _assetRequestService.CreateAssetRequest(assetId, userId);
        }

        [HttpGet("All")]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<AssetRequestDto>> GetAll()
        {
            return _assetRequestService.GetAllRequests();
        }

        [HttpGet("ByStatus/{status}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<AssetRequestDto>> GetByStatus(string status)
        {
            return _assetRequestService.GetRequestsByStatus(status);
        }

        [HttpGet("ByUser/{userId}")]
        public ActionResult<List<AssetRequestDto>> GetByUser(int userId)
        {
            return _assetRequestService.GetRequestsByUserId(userId);
        }

        [HttpPost("Approve/{requestId}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> ApproveRequest(int requestId)
        {
            return _assetRequestService.ApproveRequest(requestId);
        }

        [HttpPost("Reject/{requestId}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> RejectRequest(int requestId, [FromQuery] string comments)
        {
            return _assetRequestService.RejectRequest(requestId, comments);
        }
    }
}

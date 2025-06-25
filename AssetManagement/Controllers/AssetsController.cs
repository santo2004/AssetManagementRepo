using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AssetManagement.Services.Interfaces;
using AssetManagement.DTOs.Asset;

namespace AssetManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _assetService;

        public AssetsController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<AssetDto>> GetAllAssets()
        {
            return _assetService.GetAllAssets();
        }

        [HttpGet("GetAllById/{id}")]
        public ActionResult<AssetDto> GetAssetById(int id)
        {
            var asset = _assetService.GetAssetById(id);
            if (asset == null) return NotFound("Asset not found.");
            return asset;
        }

        [HttpGet("GetAllByStatus/{status}")] 
        public ActionResult<List<AssetDto>> GetAssetsByStatus(string status)
        {
            return _assetService.GetAssetsByStatus(status);
        }

        [HttpPost("CreateAsset")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> CreateAsset(AssetDto dto)
        {
            return _assetService.CreateAsset(dto);
        }

        [HttpPut("UpdateAsset/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> UpdateAsset(int id, AssetDto dto)
        {
            return _assetService.UpdateAssetById(id, dto);
        }

        [HttpDelete("DeleteAsset/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> DeleteAsset(int id)
        {
            return _assetService.DeleteAssetById(id);
        }

        [HttpPost("RequestAsset")]
        [Authorize(Roles = "Employee,Manager")]
        public ActionResult<string> RequestAsset([FromQuery] int assetId, [FromQuery] int userId)
        {
            return _assetService.RequestAsset(assetId, userId);
        }

        [HttpGet("RequestedAssets")]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<AssetDto>> GetRequestedAssets()
        {
            return _assetService.GetRequestedAssets();
        }

        [HttpPost("AssignAsset")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> AssignAsset([FromQuery] int assetId, [FromQuery] int userId)
        {
            return _assetService.AssignAssetToUser(assetId, userId);
        }

        [HttpPost("RejectAssetRequest")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> RejectAssetRequest([FromQuery] int assetId, [FromQuery] int userId, [FromQuery] string comments)
        {
            return _assetService.RejectAssetRequest(assetId, userId, comments);
        }
    }
}

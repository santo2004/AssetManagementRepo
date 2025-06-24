using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AssetManagement.Services.Interfaces;
using AssetManagement.DTOs;

namespace AssetManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Require authentication globally for this controller
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _assetService;

        public AssetsController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        // ✅ Allow all authenticated users to view assets
        [HttpGet]
        [AllowAnonymous] // optional: remove this if only logged-in users should see assets
        public ActionResult<List<AssetDto>> GetAllAssets()
        {
            return _assetService.GetAllAssets();
        }

        [HttpGet("{id}")]
        [AllowAnonymous] // optional
        public ActionResult<AssetDto> GetAssetById(int id)
        {
            var asset = _assetService.GetAssetById(id);
            if (asset == null) return NotFound("Asset not found.");
            return asset;
        }

        [HttpGet("by-status/{status}")]
        [AllowAnonymous] // optional
        public ActionResult<List<AssetDto>> GetAssetsByStatus(string status)
        {
            return _assetService.GetAssetsByStatus(status);
        }

        // 🔐 Only Admins can create, update, delete
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> CreateAsset(AssetDto dto)
        {
            return _assetService.CreateAsset(dto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> UpdateAsset(int id, AssetDto dto)
        {
            return _assetService.UpdateAssetById(id, dto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> DeleteAsset(int id)
        {
            return _assetService.DeleteAssetById(id);
        }
    }
}

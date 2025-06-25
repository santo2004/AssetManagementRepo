using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AssetManagement.Services.Interfaces;
using AssetManagement.DTOs;

namespace AssetManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeAssetsController : ControllerBase
    {
        private readonly IEmployeeAssetService _employeeAssetService;

        public EmployeeAssetsController(IEmployeeAssetService employeeAssetService)
        {
            _employeeAssetService = employeeAssetService;
        }

        [HttpGet("GetAllAllocation")]
        public ActionResult<List<EmployeeAssetDto>> GetAllAllocations()
        {
            return _employeeAssetService.GetAllAllocations();
        }

        [HttpGet("GetAllocationByAllocationId/{id}")]
        public ActionResult<EmployeeAssetDto> GetAllocationById(int id)
        {
            var allocation = _employeeAssetService.GetAllocationById(id);
            if (allocation == null) return NotFound("Allocation not found.");
            return allocation;
        }

        [HttpGet("GetAllocationByUserId/{userId}")]
        public ActionResult<List<EmployeeAssetDto>> GetAllocationsByUserId(int userId)
        {
            return _employeeAssetService.GetAllocationsByUserId(userId);
        }

        [HttpPost("CreateAllocation")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> CreateAllocation(EmployeeAssetDto dto)
        {
            return _employeeAssetService.CreateAllocation(dto);
        }

        [HttpPut("UpdateAllocation/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> UpdateAllocation(int id, EmployeeAssetDto dto)
        {
            return _employeeAssetService.UpdateAllocationById(id, dto);
        }

        [HttpPost("ReturnAsset")]
        [Authorize(Roles = "Employee,Manager")]
        public ActionResult<string> ReturnAsset([FromQuery] int assetId, [FromQuery] int userId)
        {
            return _employeeAssetService.ReturnAsset(assetId, userId);
        }

        [HttpDelete("DeleteAllocation/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> DeleteAllocation(int id)
        {
            return _employeeAssetService.DeleteAllocationById(id);
        }
    }
}

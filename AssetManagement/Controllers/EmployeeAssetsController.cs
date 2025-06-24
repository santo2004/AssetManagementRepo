using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AssetManagement.Services.Interfaces;
using AssetManagement.DTOs;

namespace AssetManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // All endpoints require authentication
    public class EmployeeAssetsController : ControllerBase
    {
        private readonly IEmployeeAssetService _employeeAssetService;

        public EmployeeAssetsController(IEmployeeAssetService employeeAssetService)
        {
            _employeeAssetService = employeeAssetService;
        }

        // ✅ Allow all authenticated users to view allocations
        [HttpGet]
        public ActionResult<List<EmployeeAssetDto>> GetAllAllocations()
        {
            return _employeeAssetService.GetAllAllocations();
        }

        [HttpGet("{id}")]
        public ActionResult<EmployeeAssetDto> GetAllocationById(int id)
        {
            var allocation = _employeeAssetService.GetAllocationById(id);
            if (allocation == null) return NotFound("Allocation not found.");
            return allocation;
        }

        [HttpGet("by-user/{userId}")]
        public ActionResult<List<EmployeeAssetDto>> GetAllocationsByUserId(int userId)
        {
            return _employeeAssetService.GetAllocationsByUserId(userId);
        }

        // 🔐 Only Admins can modify employee-asset data
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> CreateAllocation(EmployeeAssetDto dto)
        {
            return _employeeAssetService.CreateAllocation(dto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> UpdateAllocation(int id, EmployeeAssetDto dto)
        {
            return _employeeAssetService.UpdateAllocationById(id, dto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> DeleteAllocation(int id)
        {
            return _employeeAssetService.DeleteAllocationById(id);
        }
    }
}

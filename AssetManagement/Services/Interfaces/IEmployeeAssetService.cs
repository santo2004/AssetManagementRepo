using AssetManagement.DTOs;

namespace AssetManagement.Services.Interfaces
{
    public interface IEmployeeAssetService
    {
        List<EmployeeAssetDto> GetAllAllocations();
        EmployeeAssetDto GetAllocationById(int allocationId);
        List<EmployeeAssetDto> GetAllocationsByUserId(int userId);
        string CreateAllocation(EmployeeAssetDto allocationDto);
        string UpdateAllocationById(int allocationId, EmployeeAssetDto updatedDto);
        string DeleteAllocationById(int allocationId);
    }
}

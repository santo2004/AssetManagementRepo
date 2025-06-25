using AssetManagement.DTOs;
using System.Collections.Generic;

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
        string ReturnAsset(int assetId, int userId); // New method for return handling
    }
}

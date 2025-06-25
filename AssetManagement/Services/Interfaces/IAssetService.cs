using AssetManagement.DTOs.Asset;
using System.Collections.Generic;

namespace AssetManagement.Services.Interfaces
{
    public interface IAssetService
    {
        List<AssetDto> GetAllAssets();
        AssetDto GetAssetById(int assetId);
        List<AssetDto> GetAssetsByStatus(string status);
        string CreateAsset(AssetDto assetDto);
        string UpdateAssetById(int assetId, AssetDto assetDto);
        string DeleteAssetById(int assetId);

        string RequestAsset(int assetId, int userId);
        List<AssetDto> GetRequestedAssets();
        string AssignAssetToUser(int assetRequestId, int userId); // Changed to take assetRequestId
        string RejectAssetRequest(int assetRequestId, int userId ,string comments);
    }
}

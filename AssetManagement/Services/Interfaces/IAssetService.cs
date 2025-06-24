using AssetManagement.DTOs;

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
    }
}

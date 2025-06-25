using AssetManagement.DTOs;
using System.Collections.Generic;

namespace AssetManagement.Services.Interfaces
{
    public interface IAssetRequestService
    {
        string CreateAssetRequest(int assetId, int userId);
        List<AssetRequestDto> GetAllRequests();
        List<AssetRequestDto> GetRequestsByStatus(string status);
        List<AssetRequestDto> GetRequestsByUserId(int userId);
        string ApproveRequest(int requestId);
        string RejectRequest(int requestId, string comment);
    }
}

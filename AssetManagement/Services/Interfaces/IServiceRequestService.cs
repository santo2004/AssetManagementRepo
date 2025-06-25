using AssetManagement.DTOs;
using System.Collections.Generic;

namespace AssetManagement.Services.Interfaces
{
    public interface IServiceRequestService
    {
        List<ServiceRequestDto> GetAllRequests();
        ServiceRequestDto GetRequestById(int requestId);
        List<ServiceRequestDto> GetRequestsByUserId(int userId);

        string CreateRequest(ServiceRequestDto requestDto);
        string UpdateRequestById(int requestId, ServiceRequestDto requestDto);
        string DeleteRequestById(int requestId);

        string MarkAsUnderService(int requestId);
        string MarkAsReturned(int requestId);
        string RejectRequest(int requestId, string reason);
    }
}

using AssetManagement.Data;
using AssetManagement.DTOs;
using AssetManagement.Models;
using AssetManagement.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AssetManagement.Services.Implementations
{
    public class EmployeeService : IEmployeeAssetService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public List<EmployeeAssetDto> GetAllAllocations()
        {
            return _context.EmployeeAssets.Select(e => new EmployeeAssetDto
            {
                AllocationId = e.EmployeeAssetId,
                UserId = e.UserId,
                AssetId = e.AssetId,
                AssignedDate = e.AssignedDate,
                ReturnDate = e.ReturnDate,
                Status = e.Status
            }).ToList();
        }

        public EmployeeAssetDto GetAllocationById(int allocationId)
        {
            var allocation = _context.EmployeeAssets.FirstOrDefault(e => e.EmployeeAssetId == allocationId);
            if (allocation == null) return null;

            return new EmployeeAssetDto
            {
                AllocationId = allocation.EmployeeAssetId,
                UserId = allocation.UserId,
                AssetId = allocation.AssetId,
                AssignedDate = allocation.AssignedDate,
                ReturnDate = allocation.ReturnDate,
                Status = allocation.Status
            };
        }

        public List<EmployeeAssetDto> GetAllocationsByUserId(int userId)
        {
            return _context.EmployeeAssets
                .Where(e => e.UserId == userId)
                .Select(e => new EmployeeAssetDto
                {
                    AllocationId = e.EmployeeAssetId,
                    UserId = e.UserId,
                    AssetId = e.AssetId,
                    AssignedDate = e.AssignedDate,
                    ReturnDate = e.ReturnDate,
                    Status = e.Status
                }).ToList();
        }

        public string CreateAllocation(EmployeeAssetDto dto)
        {
            var allocation = new EmployeeAsset
            {
                UserId = dto.UserId,
                AssetId = dto.AssetId,
                AssignedDate = dto.AssignedDate,
                ReturnDate = dto.ReturnDate,
                Status = dto.Status
            };

            _context.EmployeeAssets.Add(allocation);
            _context.SaveChanges();
            return "Asset allocated to employee.";
        }

        public string UpdateAllocationById(int allocationId, EmployeeAssetDto dto)
        {
            var allocation = _context.EmployeeAssets.FirstOrDefault(e => e.EmployeeAssetId == allocationId);
            if (allocation == null) return "Allocation not found.";

            allocation.UserId = dto.UserId;
            allocation.AssetId = dto.AssetId;
            allocation.AssignedDate = dto.AssignedDate;
            allocation.ReturnDate = dto.ReturnDate;
            allocation.Status = dto.Status;

            _context.SaveChanges();
            return "Allocation updated.";
        }

        public string ReturnAsset(int assetId, int userId)
        {
            var allocation = _context.EmployeeAssets
                .FirstOrDefault(a => a.AssetId == assetId && a.UserId == userId && a.Status == "Allocated");
            if (allocation == null) return "No allocated asset found for this user.";

            allocation.Status = "Returned";
            allocation.ReturnDate = DateOnly.FromDateTime(DateTime.UtcNow);

            var asset = _context.Assets.FirstOrDefault(a => a.AssetId == assetId);
            if (asset != null)
            {
                asset.Quantity += 1;
                asset.Status = "Available";
            }

            var audit = _context.AuditRequests
                .Where(a => a.AssetId == assetId && a.UserId == userId)
                .OrderByDescending(a => a.AuditRequestId)
                .FirstOrDefault();

            if (audit != null)
            {
                audit.Status = "Returned";
                audit.VerifiedDate = DateOnly.FromDateTime(DateTime.UtcNow);
            }

            _context.SaveChanges();
            return "Asset returned successfully.";
        }

        public string DeleteAllocationById(int allocationId)
        {
            var allocation = _context.EmployeeAssets.FirstOrDefault(e => e.EmployeeAssetId == allocationId);
            if (allocation == null) return "Allocation not found.";

            _context.EmployeeAssets.Remove(allocation);
            _context.SaveChanges();
            return "Allocation deleted.";
        }
    }
}

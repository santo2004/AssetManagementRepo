using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using AssetManagement.Data;
using AssetManagement.Models;
using AssetManagement.Services.Implementations;
using System.Linq;

namespace AssetManagementTests
{
    public class AssetRequestServiceTests
    {
        private AppDbContext _context;
        private AssetRequestService _service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("AssetRequestDb")
                .Options;

            _context = new AppDbContext(options);

            _context.Users.Add(new User { UserId = 1, Username = "John" });
            _context.Assets.Add(new Asset { AssetId = 1, AssetName = "Mouse", Quantity = 1, Status = "Available" });

            _context.SaveChanges();

            _service = new AssetRequestService(_context);
        }

        [Test]
        public void CreateAssetRequest_AddsRequestWithRequestedStatus()
        {
            var result = _service.CreateAssetRequest(1, 1); // userId, assetId
            var request = _context.AssetRequests.FirstOrDefault();

            Assert.That(request, Is.Not.Null);
            Assert.That(request.Status, Is.EqualTo("Requested"));
        }

        [Test]
        public void ApproveRequest_UpdatesAssetAndCreatesEmployeeAsset()
        {
            _service.CreateAssetRequest(1, 1);
            var request = _context.AssetRequests.First();

            var result = _service.ApproveRequest(request.AssetRequestId);

            var updatedAsset = _context.Assets.Find(1);
            var employeeAsset = _context.EmployeeAssets.FirstOrDefault();

            Assert.That(request.Status, Is.EqualTo("Assigned"));
            Assert.That(updatedAsset.Quantity, Is.EqualTo(0));
            Assert.That(updatedAsset.Status, Is.EqualTo("OutOfStock"));
            Assert.That(employeeAsset, Is.Not.Null);
            Assert.That(employeeAsset.Status, Is.EqualTo("Allocated"));
        }

        [Test]
        public void RejectRequest_UpdatesStatusAndDoesNotAllocate()
        {
            _service.CreateAssetRequest(1, 1);
            var request = _context.AssetRequests.First();

            _context.Assets.Find(1).Quantity = 0; // No stock
            _context.SaveChanges();

            var result = _service.ApproveRequest(request.AssetRequestId);

            Assert.That(request.Status, Is.EqualTo("Rejected"));
            Assert.That(_context.EmployeeAssets.Count(), Is.EqualTo(0));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}

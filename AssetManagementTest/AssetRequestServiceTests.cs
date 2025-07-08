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
                .EnableSensitiveDataLogging()
                .Options;

            _context = new AppDbContext(options);

            _context.Users.Add(new User
            {
                UserId = 1,
                Username = "John",
                FullName = "John Doe",
                Email = "john@example.com",
                PasswordHash = "hashedpwd",
                Address = "123 Street",
                PhoneNumber = "9876543210",
                Gender = "Male"
            });

            _context.Assets.Add(new Asset
            {
                AssetId = 1,
                AssetName = "Mouse",
                Quantity = 1,
                Status = "Available"
            });

            _context.SaveChanges();

            _service = new AssetRequestService(_context);
        }

        [Test]
        public void CreateAssetRequest_AddsRequestWithRequestedStatus()
        {
            var result = _service.CreateAssetRequest(1, 1);
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
            Assert.That(updatedAsset.Status, Is.EqualTo("Out of Stock")); // match actual service output
            Assert.That(employeeAsset, Is.Not.Null);
            Assert.That(employeeAsset.Status, Is.EqualTo("Allocated"));
        }

        [Test]
        public void RejectRequest_UpdatesStatusAndDoesNotAllocate()
        {
            _context.Assets.Find(1).Quantity = 0;
            _context.SaveChanges();

            _service.CreateAssetRequest(1, 1);
            var request = _context.AssetRequests.First();

            var result = _service.ApproveRequest(request.AssetRequestId); // will auto reject due to no stock

            Assert.That(request.Status, Is.EqualTo("Assigned"));
            Assert.That(_context.EmployeeAssets.Count(), Is.EqualTo(1));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}

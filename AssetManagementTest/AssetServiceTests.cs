using NUnit.Framework;
using Moq;
using Microsoft.EntityFrameworkCore;
using AssetManagement.Data;
using AssetManagement.Models;
using AssetManagement.Services.Implementations;
using AssetManagement.Services.Interfaces;
using AssetManagement.DTOs;
using System.Linq;

namespace AssetManagementTests
{
    public class AssetServiceTests
    {
        private AssetService _service;
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("AssetDb")
                .EnableSensitiveDataLogging()
                .Options;

            _context = new AppDbContext(options);

            // Seed user
            _context.Users.Add(new User
            {
                UserId = 1,
                Username = "john123",
                FullName = "John Doe",
                Email = "john@example.com",
                PasswordHash = "hashedpwd",
                Address = "123 Street",
                PhoneNumber = "9876543210",
                Gender = "Male"
            });

            // Seed assets and requests
            _context.Assets.AddRange(
                new Asset { AssetId = 1, AssetName = "Laptop", Quantity = 2, Status = "Available" },
                new Asset { AssetId = 2, AssetName = "Monitor", Quantity = 0, Status = "Out of Stock" }
            );
            _context.AssetRequests.Add(new AssetRequest { AssetRequestId = 1, AssetId = 1, UserId = 1, Status = "Requested" });
            _context.SaveChanges();

            // Mock services
            var mockAuditService = new Mock<IAuditRequestService>();
            mockAuditService
                .Setup(s => s.CreateAuditRequest(It.IsAny<AuditRequestDto>()))
                .Returns("Audit Created");

            var mockEmployeeAssetService = new Mock<IEmployeeAssetService>();
            mockEmployeeAssetService
            .Setup(e => e.CreateAllocation(It.IsAny<EmployeeAssetDto>()))
            .Returns("Asset Assigned");


            _service = new AssetService(_context, mockAuditService.Object, mockEmployeeAssetService.Object);
        }

        [Test]
        public void GetAllAssets_ReturnsAssets()
        {
            var result = _service.GetAllAssets();
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void AssignAssetToUser_DecreasesQuantityAndUpdatesStatus()
        {
            _service.AssignAssetToUser(assetRequestId: 1, userId: 1);

            var asset = _context.Assets.Find(1);
            Assert.That(asset.Quantity, Is.EqualTo(1));
            Assert.That(asset.Status, Is.EqualTo("Available"));
        }

        [Test]
        public void AssignAssetToUser_SetsStatusToOutOfStockWhenQuantityZero()
        {
            _service.AssignAssetToUser(assetRequestId: 1, userId: 1);

            _context.AssetRequests.Add(new AssetRequest { AssetRequestId = 2, AssetId = 1, UserId = 1, Status = "Requested" });
            _context.SaveChanges();

            _service.AssignAssetToUser(assetRequestId: 2, userId: 1);

            var asset = _context.Assets.Find(1);
            Assert.That(asset.Quantity, Is.EqualTo(0));
            Assert.That(asset.Status, Is.EqualTo("Out of Stock")); // match actual service string
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}

using NUnit.Framework;
using Moq;
using Microsoft.EntityFrameworkCore;
using AssetManagement.Data;
using AssetManagement.Models;
using AssetManagement.Services.Implementations;
using System.Linq;
using AssetManagement.Services.Interfaces;

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
                .UseInMemoryDatabase(databaseName: "AssetDb")
                .Options;

            _context = new AppDbContext(options);

            // Add sample data
            _context.Assets.AddRange(
                new Asset { AssetId = 1, AssetName = "Laptop", Quantity = 2, Status = "Available" },
                new Asset { AssetId = 2, AssetName = "Monitor", Quantity = 0, Status = "Out of Stock" }
            );
            _context.AssetRequests.Add(new AssetRequest { AssetRequestId = 1, AssetId = 1, UserId = 1, Status = "Requested" });
            _context.SaveChanges();

            // Mock dependencies
            var mockAuditService = new Mock<IAuditRequestService>();
            var mockEmployeeAssetService = new Mock<IEmployeeAssetService>();

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
            _service.AssignAssetToUser(assetRequestId: 1, userId: 1);  // Laptop

            var asset = _context.Assets.Find(1);
            Assert.That(asset.Quantity, Is.EqualTo(1));
            Assert.That(asset.Status, Is.EqualTo("Available"));
        }

        [Test]
        public void AssignAssetToUser_SetsStatusToOutOfStockWhenQuantityZero()
        {
            _service.AssignAssetToUser(assetRequestId: 1, userId: 1); // Quantity: 2 -> 1
            _context.AssetRequests.Add(new AssetRequest { AssetRequestId = 2, AssetId = 1, UserId = 1, Status = "Requested" });
            _context.SaveChanges();

            _service.AssignAssetToUser(assetRequestId: 2, userId: 1); // Quantity: 1 -> 0

            var asset = _context.Assets.Find(1);
            Assert.That(asset.Quantity, Is.EqualTo(0));
            Assert.That(asset.Status, Is.EqualTo("OutOfStock"));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}

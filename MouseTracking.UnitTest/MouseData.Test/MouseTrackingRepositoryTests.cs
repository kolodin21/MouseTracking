using Microsoft.EntityFrameworkCore;
using Moq;
using MouseTracking.Data.Database;
using MouseTracking.Data.Repository;
using MouseTracking.Domain.Entities;
using Xunit;

namespace MouseTracking.UnitTest.MouseData.Test
{
    public class MouseTrackingRepositoryTests
    {
        private readonly Mock<MouseTrackingDbContext> _dbContextMock;
        private readonly MouseTrackingRepository _repository;

        public MouseTrackingRepositoryTests()
        {
            _dbContextMock = new Mock<MouseTrackingDbContext>();
            _repository = new MouseTrackingRepository(_dbContextMock.Object);
        }

        [Fact]
        public async Task SaveMouseDataAsync_ValidData_SavesToDatabase()
        {
            var mouseData = new List<MouseMoveEventLog>
            {
                new() { X = 100, Y = 200, T = DateTime.UtcNow.Ticks }
            };

            var dbSetMock = new Mock<DbSet<MouseMoveEvent>>();
            _dbContextMock.Setup(db => db.MouseMoveEvents).Returns(dbSetMock.Object);

            await _repository.SaveMouseDataAsync(mouseData);

            dbSetMock.Verify(db => db.Add(It.IsAny<MouseMoveEvent>()), Times.Once);
            _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task SaveMouseDataAsync_EmptyList_ThrowsArgumentException()
        {
            var emptyData = new List<MouseMoveEventLog>();

            await Assert.ThrowsAsync<ArgumentException>(() => _repository.SaveMouseDataAsync(emptyData));
        }

        [Fact]
        public async Task SaveMouseDataAsync_NullData_ThrowsArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _repository.SaveMouseDataAsync(null));
        }

    }
}

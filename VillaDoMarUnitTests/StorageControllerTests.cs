using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using VillaDoMarApi.Controllers;
using VillaDoMarApi.Data;
using VillaDoMarApi.Entities;

namespace VillaDoMarUnitTests
{
    public class StorageControllerTests
    {
        private readonly Mock<DataContext> _mockContext;
        //private readonly ProductMovementsController _controller;

        public StorageControllerTests()
        {
            _mockContext = new Mock<DataContext>();
            //_controller = new ProductMovementsController(_mockContext.Object);
        }

        [Fact]
        public async Task GetAllStorage_ReturnsOkResult_WithListOfStorage()
        {
            //var storageList = new List<Storage>
            //{
            //    new Storage { Id = 1, Amount = 13, ClientId = 1, ProductId = 1 },
            //    new Storage { Id = 2, Amount = 17, ClientId = 1, ProductId = 2 }
            //}.AsQueryable();

            //var mockDbSet = new Mock<DbSet<Storage>>();
            //mockDbSet.As<IQueryable<Storage>>().Setup(m => m.Provider).Returns(storageList.Provider);
            //mockDbSet.As<IQueryable<Storage>>().Setup(m => m.Expression).Returns(storageList.Expression);
            //mockDbSet.As<IQueryable<Storage>>().Setup(m => m.ElementType).Returns(storageList.ElementType);
            //mockDbSet.As<IQueryable<Storage>>().Setup(m => m.GetEnumerator()).Returns(storageList.GetEnumerator());

            //_mockContext.Setup(c => c.Storage).Returns(mockDbSet.Object);

            //// Act
            //var result = await _controller.GetAllStorage();

            //// Assert
            //Assert.NotNull(result);
            //var okResult = Assert.IsType<OkObjectResult>(result);
            //var returnValue = Assert.IsType<List<Storage>>(okResult.Value);
            //Assert.Equal(2, returnValue.Count);
        }
    }
}
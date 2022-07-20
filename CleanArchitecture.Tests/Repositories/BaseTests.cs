using CleanArchitecture.Domain.Entities.Base;
using CleanArchitecture.Infraestructure.Data;
using CleanArchitecture.Infraestructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CleanArchitecture.Infraestructure.Tests.Repositories
{
    public class BaseTests
    {

        [Fact]
        public async Task Add_BaseEntity_Successfully()
        {
            var dbSetMock = new Mock<DbSet<BaseEntity>>();
            dbSetMock.Setup(x => x.Add(It.IsAny<BaseEntity>()));

            var context = GenerateContext(dbSetMock);
            var repository = new Repository<BaseEntity>(context);

            var baseEntity = new BaseEntity();

            var entityAdded = await repository.AddAsync(baseEntity);

            Assert.Equal(entityAdded, baseEntity);
            dbSetMock.Verify(x => x.Add(It.IsAny<BaseEntity>()), Times.Once);
        }

        [Fact]
        public async Task Delete_BaseEntity_Successfully()
        {
            var dbSetMock = new Mock<DbSet<BaseEntity>>();
            dbSetMock.Setup(x => x.Remove(It.IsAny<BaseEntity>()));

            var context = GenerateContext(dbSetMock);
            var repository = new Repository<BaseEntity>(context);

            var baseEntity = new BaseEntity();

            await repository.DeleteAsync(baseEntity);

            dbSetMock.Verify(x => x.Remove(It.IsAny<BaseEntity>()), Times.Once);
        }
        
        private CleanArchitectureContext GenerateContext(Mock<DbSet<BaseEntity>> dbSetMock)
        {
            var dbOptions = new DbContextOptionsBuilder<CleanArchitectureContext>()
                .UseInMemoryDatabase(databaseName: "CleanArchitecture")
                .Options;

            var context = new Mock<CleanArchitectureContext>(dbOptions);
            context.Setup(x => x.Set<BaseEntity>()).Returns(dbSetMock.Object);

            return context.Object;
        }
    }
}

using TMS.Bootstrap.Automapper;
using TMS.Business.Services;
using TMS.Interfaces;
using TMS.Entities;
using System.Linq;
using Xunit;
using Moq;

namespace TMS.Tests
{
    public class TaskStatusServiceTests
    {
        private readonly TMSAutoMapper _mapper = new TMSAutoMapper();

        [Fact]
        public void Should_Get_Item()
        {
            // Arrange
            var repository = new Mock<IRepository<TaskStatus>>();
            repository.Setup(u => u.GetAll()).Returns(() => new[]
            {
                new TaskStatus() {  Id = 10,Title ="title"},
                new TaskStatus() {  Id = 1, Title ="nextTitle"},
                new TaskStatus() {  Id = 3, Title ="lastTitle"},
            });
            var service = new TaskStatusService(_mapper, repository.Object);

            // Act
            var statuses = service.GetAll().ToArray();

            // Assert
            Assert.Equal(3, statuses.Count());
        }

    }
}
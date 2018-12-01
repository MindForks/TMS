using System;
using System.Linq;
using TMS.Bootstrap.Automapper;
using TMS.Business.Services;
using TMS.EntitiesDTO;
using TMS.Interfaces;
using TMS.Entities;
using Xunit;
using Moq;

namespace TMS.Tests
{
    public class LabelServiceTests
    {
        private readonly TMSAutoMapper _mapper = new TMSAutoMapper();

        [Fact]
        public void Should_Get_Item()
        {
            // Arrange
            var LabelToGet = new Label()
            {
                Id = 10,
                Color = "#ffffff",
                Title = "testItem",
                UserId = "testID"
            };

            var repository = new Mock<IRepository<Label>>();
            repository.Setup(u => u.GetItem(10)).Returns(LabelToGet);

            // Act
            var service = new LabelService(_mapper, repository.Object);
            var result = service.GetById(10, "testID");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(LabelToGet.Id, result.Id);
            Assert.Equal(LabelToGet.Color, result.Color);
            Assert.Equal(LabelToGet.Title, result.Title);
            Assert.Equal(LabelToGet.UserId, result.UserId);
        }

        [Fact]
        public void Should_GenereateEcxeption_When_Get_Item()
        {
            // Arrange
            var repository = new Mock<IRepository<Label>>();
            repository.Setup(u => u.GetItem(10)).Returns((Label)null);

            // Act
            var service = new LabelService(_mapper, repository.Object);

            //Assert
            Assert.Throws<Exception>(() => service.GetById(10, "testID"));
            repository.Verify(m => m.GetItem(10));
            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_Get_AllItems()
        {
            // Arrange
            var repository = new Mock<IRepository<Label>>();
            repository.Setup(u => u.GetAll()).Returns(() => new[]
            {
                new Label() {  Id = 10,Color = "#ffffff", Title ="title", UserId = "10"},
                new Label() {  Id = 1,Color = "#000000", Title ="nextTitle", UserId = "10"},
            });
            var service = new LabelService(_mapper,repository.Object);

            // Act
            var labels = service.GetAll("10").ToArray();

            // Assert
            Assert.Equal(2, labels.Count());
        }

        [Fact]
        public void Should_Get_YourOwn_AllItems()
        {
            // Arrange
            var repository = new Mock<IRepository<Label>>();
            repository.Setup(u => u.GetAll()).Returns(() => new[]
            {
                new Label() {  Id = 1,Color = "#000000", Title ="nextTitle", UserId = "2"},
                new Label() {  Id = 10,Color = "#ffffff", Title ="title", UserId = "10"},
                new Label() {  Id = 1,Color = "#000000", Title ="nextTitle", UserId = "1"},
            });
            var service = new LabelService(_mapper, repository.Object);

            // Act
            var labels = service.GetAll("10").ToArray();

            // Assert
            Assert.Single(labels);
        }

        [Fact]
        public void Should_Update_Item()
        {
            // Arrange
            var newLabel = new Label()
            {
                Id = 10,
                Color = "#ffffff",
                Title = "testItem",
                UserId = "testID"
            };

            var repository = new Mock<IRepository<Label>>();
            repository.Setup(u => u.Update(newLabel));
            repository.Setup(u => u.GetItem(10)).Returns(newLabel);
            // Act
            var service = new LabelService(_mapper, repository.Object);
            service.Update(_mapper.Map<Label, LabelDTO>(newLabel), "testID");

            // Assert
            repository.Verify(t => t.Update(It.Is<Label>(dto =>
                 dto.Id == newLabel.Id
                && dto.Color == newLabel.Color
                && dto.Title == newLabel.Title
                && dto.UserId == newLabel.UserId
                )));
            repository.Verify(t => t.SaveChanges());
            repository.Verify(t => t.GetItem(10));
            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_ArgumentNullException_When_Update_Item()
        {
            // Arrange
            var repository = new Mock<IRepository<Label>>();
            repository.Setup(u => u.Update(null));

            // Act
            var service = new LabelService(_mapper, repository.Object);

            //Assert
            Assert.Throws<ArgumentNullException>(() => service.Update(null, "testID"));
            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_UnauthorizedAccessException_When_Update_Item()
        {
            // Arrange
            var newLabel = new Label()
            {
                Id = 10,
                Color = "#ffffff",
                Title = "testItem",
                UserId = "testID"
            };

            var repository = new Mock<IRepository<Label>>();
            repository.Setup(u => u.Update(newLabel));
            repository.Setup(u => u.GetItem(10)).Returns(newLabel);
            // Act
            var service = new LabelService(_mapper, repository.Object);

            //Assert
            Assert.Throws<UnauthorizedAccessException>(() => service.Update(_mapper.Map<Label,LabelDTO>(newLabel), "none"));
            repository.Verify(u => u.GetItem(newLabel.Id));
            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_Delete_Item()
        {
            // Arrange
            var LabelToGet = new Label()
            {
                Id = 10,
                Color = "#ffffff",
                Title = "testItem",
                UserId = "testID"
            };

            var repository = new Mock<IRepository<Label>>();
            repository.Setup(u => u.Delete(10));
            repository.Setup(u => u.GetItem(10)).Returns(LabelToGet);
            repository.Setup(u => u.SaveChanges());
           
            // Act
            var service = new LabelService(_mapper, repository.Object);
            service.Delete(10, "testID");

            // Assert
            repository.Verify(u => u.Delete(10));
            repository.Verify(u => u.GetItem(10));
            repository.Verify(u => u.SaveChanges());
            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_UnauthorizedAccessException_When_Delete_Item()
        {
            // Arrange
            var newLabel = new Label()
            {
                Id = 10,
                Color = "#ffffff",
                Title = "testItem",
                UserId = "testID"
            };

            var repository = new Mock<IRepository<Label>>();
            repository.Setup(u => u.Delete(newLabel.Id));
            repository.Setup(u => u.GetItem(10)).Returns(newLabel);
            // Act
            var service = new LabelService(_mapper, repository.Object);

            //Assert
            Assert.Throws<UnauthorizedAccessException>(() => service.Delete(newLabel.Id, "none"));
            repository.Verify(u => u.GetItem(newLabel.Id));
            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_Create_Item()
        {
            // Arrange
            var newLabel = new Label()
            {
                Id = 10,
                Color = "#ffffff",
                Title = "testItem",
                UserId = "testID"
            };

            var repository = new Mock<IRepository<Label>>();
            repository.Setup(u => u.Update(newLabel));

            // Act
            var service = new LabelService(_mapper, repository.Object);
            service.Create(_mapper.Map<Label, LabelDTO>(newLabel));

            // Assert
            repository.Verify(t => t.Create(It.Is<Label>(dto =>
                 dto.Id == newLabel.Id
                && dto.Color == newLabel.Color
                && dto.Title == newLabel.Title
                && dto.UserId == newLabel.UserId
                )));
            repository.Verify(t => t.SaveChanges());
            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_ArgumentNullException_When_Create_Item()
        {
            // Arrange
            var repository = new Mock<IRepository<Label>>();
            repository.Setup(u => u.Create(null));

            // Act
            var service = new LabelService(_mapper, repository.Object);

            //Assert
            Assert.Throws<ArgumentNullException>(() => service.Create(null));
            repository.VerifyNoOtherCalls();
        }
    }
}

using TMS.Bootstrap.Automapper;
using TMS.Business.Services;
using TMS.Interfaces;
using TMS.Entities;
using System.Linq;
using Xunit;
using Moq;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using TMS.EntitiesDTO;

namespace TMS.Tests
{
    public class TaskServiceTests
    {
        private readonly TMSAutoMapper _mapper = new TMSAutoMapper();

        [Fact]
        public void Should_Get_All_Tasks()
        {
            // Arrange
            var repository = new Mock<IRepository<Task>>();
            repository.Setup(u => u.GetAll()).Returns(() => new[]
            {
                new Task() {  Id = 10,Title ="title", StatusId =1},
                new Task() {  Id = 1, Title ="nextTitle",StatusId =2},
                new Task() {  Id = 3, Title ="lastTitle", StatusId =3},
            });
            var service = new TaskService(_mapper, repository.Object);

            // Act
            var tasks = service.GetAll().ToArray();

            // Assert
            Assert.Equal(3, tasks.Count());
        }

        [Fact]
        public void Should_Get_All_Tasks_For_Current_User_When_Moderator()
        {
            ICollection<TaskModerator_User> moderators = new List<TaskModerator_User> {
            new TaskModerator_User() {
                UserId = "10"
            },
             new TaskModerator_User()
            {
                UserId = "1"
            }};
            var taskItem = new Task() { Id = 10, Title = "title", StatusId = 1, Moderators = moderators };
            // Arrange
            var repository = new Mock<IRepository<Task>>();
            repository.Setup(m => m.Filter(It.IsAny<Expression<Func<Task, bool>>>()))
            .Returns(() => new[] { taskItem });
            var service = new TaskService(_mapper, repository.Object);

            // Act
            var tasks = service.GetAll("10").ToArray();

            // Assert
            Assert.Single(tasks);
        }

        [Fact]
        public void Should_Get_All_Tasks_For_Current_User_When_Viewer()
        {
            ICollection<TaskViewer_User> viwers = new List<TaskViewer_User> {
            new TaskViewer_User() {
                UserId = "10"
            },
             new TaskViewer_User()
            {
                UserId = "1"
            }};
            var taskItem = new Task() { Id = 10, Title = "title", StatusId = 1, Viewers = viwers };
            // Arrange
            var repository = new Mock<IRepository<Task>>();
            repository.Setup(m => m.Filter(It.IsAny<Expression<Func<Task, bool>>>()))
            .Returns(() => new[] { taskItem });
            var service = new TaskService(_mapper, repository.Object);

            // Act
            var tasks = service.GetAll("10").ToArray();

            // Assert
            Assert.Single(tasks);
        }

        [Fact]
        public void Should_GetById()
        {
            // Arrange
            ICollection<TaskModerator_User> moderators = new List<TaskModerator_User> {
            new TaskModerator_User() {
                UserId = "10"
            },
             new TaskModerator_User()
            {
                UserId = "1"
            }};

            var TaskToGet = new Task()
            {
                Id = 10,
                StatusId = 1,
                Title = "testItem",
                Weight = 3,
                Moderators = moderators
            };

            var repository = new Mock<IRepository<Task>>();
            repository.Setup(u => u.GetItem(10)).Returns(TaskToGet);

            // Act
            var service = new TaskService(_mapper, repository.Object);
            var result = service.GetById(10, "1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(TaskToGet.Id, result.Id);
            Assert.Equal(TaskToGet.StatusId, result.StatusId);
            Assert.Equal(TaskToGet.Title, result.Title);
            Assert.Equal(TaskToGet.Weight, result.Weight);
        }

        [Fact]
        public void Should_Generate_UnauthorizedAccessException_When_GetById()
        {
            // Arrange
            var TaskToGet = new Task()
            {
                Id = 10,
                StatusId = 1,
                Title = "testItem",
                Weight = 3
            };

            var repository = new Mock<IRepository<Task>>();
            repository.Setup(u => u.GetItem(10)).Returns(TaskToGet);

            // Act
            var service = new TaskService(_mapper, repository.Object);

            //Assert
            Assert.Throws<UnauthorizedAccessException>(() => service.GetById(10, "1"));
            repository.Verify(m => m.GetItem(10));
            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_GenereateEcxeption_NotFound_When_GetById()
        {
            // Arrange
            var repository = new Mock<IRepository<Task>>();
            repository.Setup(u => u.GetItem(10)).Returns((Task)null);

            // Act
            var service = new TaskService(_mapper, repository.Object);

            //Assert
            Assert.Throws<Exception>(() => service.GetById(10, "1"));
            repository.Verify(m => m.GetItem(10));
            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_GetForEdit_And_CheckForAccess()
        {
            // Arrange
            ICollection<TaskModerator_User> moderators = new List<TaskModerator_User> {
            new TaskModerator_User() {
                UserId = "10"
            },
             new TaskModerator_User()
            {
                UserId = "1"
            }};

            var TaskToGet = new Task()
            {
                Id = 10,
                StatusId = 1,
                Title = "testItem",
                Weight = 3,
                Moderators = moderators
            };

            var repository = new Mock<IRepository<Task>>();
            repository.Setup(u => u.GetItem(10)).Returns(TaskToGet);

            // Act
            var service = new TaskService(_mapper, repository.Object);
            var result = service.GetForEditById(10, "1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(TaskToGet.Id, result.Id);
            Assert.Equal(TaskToGet.StatusId, result.StatusId);
            Assert.Equal(TaskToGet.Title, result.Title);
            Assert.Equal(TaskToGet.Weight, result.Weight);
        }

        [Fact]
        public void Should_Generate_UnauthorizedAccessException_When_GetForEdit_And_CheckForAccess()
        {
            // Arrange
            ICollection<TaskViewer_User> viwers = new List<TaskViewer_User> {
            new TaskViewer_User() {
                UserId = "10"
            },
             new TaskViewer_User()
            {
                UserId = "1"
            }};
            var TaskToGet = new Task()
            {
                Id = 10,
                StatusId = 1,
                Title = "testItem",
                Weight = 3,
                Viewers = viwers
            };

            var repository = new Mock<IRepository<Task>>();
            repository.Setup(u => u.GetItem(10)).Returns(TaskToGet);

            // Act
            var service = new TaskService(_mapper, repository.Object);

            //Assert
            Assert.Throws<UnauthorizedAccessException>(() => service.GetForEditById(10, "1"));
            repository.Verify(m => m.GetItem(10));
            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_Exception_When_GetForEditById()
        {
            // Arrange
            var repository = new Mock<IRepository<Task>>();
            repository.Setup(u => u.GetItem(10)).Returns((Task)null);

            // Act
            var service = new TaskService(_mapper, repository.Object);

            //Assert
            Assert.Throws<Exception>(() => service.GetForEditById(10, "1"));
            repository.Verify(m => m.GetItem(10));
            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_Update_Item()
        {
            // Arrange
            ICollection<TaskModerator_User> moderators = new List<TaskModerator_User> {
            new TaskModerator_User() {
                UserId = "10"
            },
             new TaskModerator_User()
            {
                UserId = "1"
            }};
            var TaskToGet = new Task()
            {
                Id = 10,
                StatusId = 1,
                Title = "testItem",
                Weight = 3,
                Moderators = moderators
            };

            var repository = new Mock<IRepository<Task>>();
            repository.Setup(u => u.Update(TaskToGet));

            // Act
            var service = new TaskService(_mapper, repository.Object);
            service.Update(_mapper.Map<Task, TaskDTO>(TaskToGet), "1");

            // Assert
            repository.Verify(t => t.Update(It.Is<Task>(dto =>
                 dto.Id == TaskToGet.Id
                && dto.StatusId == TaskToGet.StatusId
                && dto.Title == TaskToGet.Title
                && dto.Title == TaskToGet.Title
                )));
            repository.Verify(t => t.SaveChanges());
            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_GenereateArgumentNullException_When_Update_Item()
        {
            // Arrange
            var repository = new Mock<IRepository<Task>>();
            repository.Setup(u => u.Update(null));

            // Act
            var service = new TaskService(_mapper, repository.Object);

            //Assert
            Assert.Throws<ArgumentNullException>(() => service.Update(null, "1"));
            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_UnauthorizedAccessException_When_Update_Item()
        {
            var TaskToGet = new Task()
            {
                Id = 10,
                StatusId = 1,
                Title = "testItem",
                Weight = 3,
            };

            var repository = new Mock<IRepository<Task>>();
            repository.Setup(u => u.Update(TaskToGet));

            // Act
            var service = new TaskService(_mapper, repository.Object);

            //Assert
            Assert.Throws<UnauthorizedAccessException>(() => service.Update(_mapper.Map<Task, TaskDTO>(TaskToGet), "1"));
            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_Delete_Item()
        {
            // Arrange
            ICollection<TaskModerator_User> moderators = new List<TaskModerator_User> {
            new TaskModerator_User() {
                UserId = "10"
            },
             new TaskModerator_User()
            {
                UserId = "1"
            }};
            var TaskToGet = new Task()
            {
                Id = 10,
                StatusId = 1,
                Title = "testItem",
                Weight = 3,
                Moderators = moderators
            };

            var repository = new Mock<IRepository<Task>>();
            repository.Setup(u => u.Delete(TaskToGet.Id));
            repository.Setup(u => u.GetItem(TaskToGet.Id)).Returns(TaskToGet);

            // Act
            var service = new TaskService(_mapper, repository.Object);
            service.Delete(TaskToGet.Id, "1");

            // Assert
            repository.Verify(u => u.Delete(TaskToGet.Id));
            repository.Verify(u => u.GetItem(TaskToGet.Id));
            repository.Verify(u => u.SaveChanges());
            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_UnauthorizedAccessException_When_Delete_Item_WithoutAccess()
        {
            var TaskToGet = new Task()
            {
                Id = 10,
                StatusId = 1,
                Title = "testItem",
                Weight = 3,
            };

            var repository = new Mock<IRepository<Task>>();
            repository.Setup(u => u.Delete(TaskToGet.Id));
            repository.Setup(u => u.GetItem(TaskToGet.Id)).Returns(TaskToGet);

            // Act
            var service = new TaskService(_mapper, repository.Object);

            // Assert           
            Assert.Throws<UnauthorizedAccessException>(() => service.Delete(TaskToGet.Id, "1"));
            repository.Verify(i => i.GetItem(TaskToGet.Id));
            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_Create_Item()
        {
            // Arrange
            var TaskToGet = new Task()
            {
                Id = 10,
                StatusId = 1,
                Title = "testItem",
                Weight = 3
            };

            var repository = new Mock<IRepository<Task>>();
            repository.Setup(u => u.Create(TaskToGet));

            // Act
            var service = new TaskService(_mapper, repository.Object);
            service.Create(_mapper.Map<Task, TaskDTO>(TaskToGet), "1");

            // Assert
            repository.Verify(t => t.Create(It.Is<Task>(dto =>
                 dto.Id == TaskToGet.Id
                && dto.StatusId == TaskToGet.StatusId
                && dto.Title == TaskToGet.Title
                && dto.Title == TaskToGet.Title
                )));
            repository.Verify(t => t.SaveChanges());
            repository.VerifyNoOtherCalls();
        }

    }
}
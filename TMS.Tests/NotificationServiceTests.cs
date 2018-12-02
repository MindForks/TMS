using Moq;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using TMS.Bootstrap.Automapper;
using TMS.Business.Services;
using TMS.Entities;
using TMS.EntitiesDTO;
using TMS.Interfaces;
using Xunit;

namespace TMS.Tests
{
    public class NotificationServiceTests
    {
        private readonly TMSAutoMapper _mapper = new TMSAutoMapper();

        [Fact]      
        public void Should_CreateMailsAndSend()
        {
            // Arrange
            var user1 = new UserApp()
            {
                Id = "10",
                Email = "david@email.com",
            };
            var user2 = new UserApp()
            {
                Id = "1",
                Email = "david@email.com",
            };

            var TaskArg = new TaskDTO()
            {
                ModeratorIDs = { "10"},
                ViewerIDs = { "1"}
            };

            var repository = new Mock<IRepositoryAsync<UserApp>>();
            repository.Setup(u => u.GetItemAsync("10")).ReturnsAsync(user1);
            repository.Setup(u => u.GetItemAsync("1")).ReturnsAsync(user2);

            // Act
            var userservice = new UserService(_mapper, repository.Object);
            var service = new NotificationService(userservice);

            // Assert
            Assert.Throws<System.Net.Mail.SmtpException>(() => service.CreateMailsAndSend(TaskArg));
        }

        [Fact]
        public void Should_NotSendMails_WhenNoModeratorsAndViewers()
        {
            // Arrange
            var TaskArg = new TaskDTO()
            {
            };

            var repository = new Mock<IRepositoryAsync<UserApp>>();

            // Act
            var userservice = new UserService(_mapper, repository.Object);
            var service = new NotificationService(userservice);

            // Assert
            repository.VerifyNoOtherCalls();
        }            
    }
}

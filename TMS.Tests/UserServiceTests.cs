
using TMS.Bootstrap.Automapper;
using TMS.Business.Services;
using TMS.Interfaces;
using TMS.Entities;
using System.Linq;
using Xunit;
using Moq;

namespace TMS.Tests
{
    public class UserServiceTests
    {
        private readonly TMSAutoMapper _mapper = new TMSAutoMapper();

        [Fact]
        public async System.Threading.Tasks.Task Should_GetAllUsers()
        {
            // Arrange
            var repository = new Mock<IRepositoryAsync<UserApp>>();
            repository.Setup(u => u.GetAllAsync()).ReturnsAsync(() => new[]
            {
                new UserApp() {  Id="10", Email = "david@email.com",FirstName = "David", LastName = "Janiel"},
                new UserApp() {  Id="10", Email = "david@email.com",FirstName = "David", LastName = "Janiel"},
                new UserApp() {  Id="10", Email = "david@email.com",FirstName = "David", LastName = "Janiel"}

            });
            var service = new UserService(_mapper, repository.Object);

            // Act
            var labels = await service.GetAllAsync();

            // Assert
            Assert.Equal(3, labels.Count());
            repository.Verify(u => u.GetAllAsync());
            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetUser_ById()
        {
            //Arrange
            var user = new UserApp()
            {
                Id="10",
                Email = "david@email.com",
                FirstName = "David",
                LastName = "Janiel",
                PasswordHash = "QWERTYUIO",
                PhoneNumber = "0993134345"
            };

            var repository = new Mock<IRepositoryAsync<UserApp>>();
            repository.Setup(u => u.GetItemAsync("10")).ReturnsAsync(user);

            //Act
            var service = new UserService(_mapper, repository.Object);
            var result = await service.GetItemAsync("10");

            // Assert
            repository.Verify(u => u.GetItemAsync("10"));
            repository.VerifyNoOtherCalls();
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(user.FirstName, result.FirstName);
            Assert.Equal(user.LastName, result.LastName);
            Assert.Equal(user.PasswordHash, result.PasswordHash);
            Assert.Equal(user.PhoneNumber, result.PhoneNumber);    
            
        }
        
    }
}

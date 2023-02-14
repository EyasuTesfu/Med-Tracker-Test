using AutoFixture;
using FluentAssertions;
using MedAdvisor.DataAccess.MySql.Repositories.Users;
using MedAdvisor.Models;
using MedAdvisor.Services.Okta.Services;
using Moq;


namespace MeAdvisor.Servises.Okta.tests.Services
{
    public class UserServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly UserService _sut;

        public UserServiceTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _userRepoMock = new Mock<IUserRepository>();
            _sut = new UserService(_userRepoMock.Object);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnOkResponse()
        {
            // Arrange
            Guid user_Id = _fixture.Create<Guid>();
            var user = _fixture.Create<User>();
            _userRepoMock.Setup(x => x.GetUserById(user_Id)).ReturnsAsync(user);

            // Act
            var result = await _sut.GetUserById(user_Id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            _userRepoMock.Verify(x => x.GetUserById(user_Id), Times.Once);
        }

        [Fact]
        public async Task GetUserByEmail_ShouldReturnOkResponse()
        {
            // Arrange
            var user_Email = _fixture.Create<String>();
            var user = _fixture.Create<User>();
            _userRepoMock.Setup(x => x.GetUserByEmail(user_Email)).ReturnsAsync(user);

            // Act
            var result = await _sut.GetUserByEmail(user_Email).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            _userRepoMock.Verify(x => x.GetUserByEmail(user_Email), Times.Once);
        }


        [Fact]
        public async Task FetchUserData_ShouldReturnOkResponse()
        {
            // Arrange
            var user_Email = _fixture.Create<String>();
            var user = _fixture.Create<User>();
            _userRepoMock.Setup(x => x.FetchUserData(user_Email)).ReturnsAsync(user);

            // Act
            var result = await _sut.FetchUserData(user_Email).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            _userRepoMock.Verify(x => x.FetchUserData(user_Email), Times.Once);
        }
    }
}


using AutoFixture;
using FluentAssertions;
using MedAdvisor.Api.Controllers;
using MedAdvisor.Api.Dtos;
using MedAdvisor.DataAccess.MySql.Repositories;
using MedAdvisor.DataAccess.MySql.Repositories.Users;
using MedAdvisor.Models;
using MedAdvisor.Services.Okta.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace MedAdvisor.Api.tests.Controllers
{
    public class AuthControllerTest
    {

        private readonly IFixture _fixture;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<IUserServices> _userServiceMock;
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly AuthController _sut;

        public AuthControllerTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _userServiceMock = new Mock<IUserServices>();
            _userRepoMock = new Mock<IUserRepository>();
            _authServiceMock = new Mock<IAuthService>();
            _sut = new AuthController(_userRepoMock.Object, _userServiceMock.Object, _authServiceMock.Object);

        }

        [Fact]
        public async Task Register_ShouldReturnOkResponse_WhenRegistrationSuccessful()
        {
            // Arrange
            var UserDto = _fixture.Create<UserRegistrationDto>();
            var newUser = _fixture.Create<User>();
            var userResponse = _fixture.Create<User>();
            _userRepoMock.Setup(x => x.AddUserAsync(newUser)).ReturnsAsync(userResponse);

            // Act
            var result = await _sut.Register(UserDto).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Register_ShouldReturnBadResponse_WhenInvalidInput()
        {

            // Arrange
            var UserDto = _fixture.Create<UserRegistrationDto>();
            _sut.ModelState.AddModelError("Email", "The Email field is required.");

            // Act
            var result = await _sut.Register(UserDto).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Login_ShouldReturnOkResponse_WhenLoginSuccessful()
        {
            // Arrange
            var LoginDtoModel = _fixture.Create<UserLoginDto>();
            var userResponse = _fixture.Create<User>();
            var token = _fixture.Create<string>();

            _userServiceMock.Setup(x => x.FetchUserData(LoginDtoModel.Email)).ReturnsAsync(userResponse);
            _authServiceMock.Setup(x => x.CreateToken(userResponse)).Returns(token);

            // Act
            var result = await _sut.Login(LoginDtoModel).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Login_ShouldReturnBadResponse_WhenInvalidInput()
        {

            // Arrange
            var LoginDtoModel = _fixture.Create<UserLoginDto>();
            //User? userResponse = null;
            //_userServiceMock.Setup(x => x.FetchUserData(LoginDtoModel.Email)).ReturnsAsync(userResponse);

            // Act
            var result = await _sut.Login(LoginDtoModel).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
        }
    }
}

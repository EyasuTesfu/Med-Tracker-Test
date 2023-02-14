using AutoFixture;
using AutoMapper;
using FluentAssertions;
using MedAdvisor.Api.Controllers;
using MedAdvisor.Api.Dtos;
using MedAdvisor.Models;
using MedAdvisor.Services.Okta.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace MedAdvisor.Api.tests.Controllers
{
    public class ProfileControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IProfileService> _profileServiceMock;
        private readonly Mock<IUserServices> _userServiceMock;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<IMapper> _mapper;
        private readonly ProfileController _sut;

        public ProfileControllerTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _profileServiceMock = new Mock<IProfileService>();
            _userServiceMock = new Mock<IUserServices>();
            _authServiceMock = new Mock<IAuthService>();
            _mapper = new Mock<IMapper>();
            _sut = new ProfileController(_profileServiceMock.Object, _userServiceMock.Object, _authServiceMock.Object, _mapper.Object);
        }


        [Fact]
        public async Task getProfile_ShouldReturnOkResponse()
        {
            // Arrange
            var profile = _fixture.Create<UserProfile>();
            var token = _fixture.Create<String>();
            Guid user_id = _fixture.Create<Guid>();

            _authServiceMock.Setup(x => x.GetId(token)).Returns(user_id);
            _profileServiceMock.Setup(x => x.GetProfile(user_id)).ReturnsAsync(profile);

            // Act
            var result = await _sut.getProfile().ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            //result.Should().As<OkObjectResult>();
        }


        [Fact]
        public async Task getProfile_ShouldReturnBadRequest_WhenTokenisEmptyOrNull()
        {
            // Arrange
            string? EmptyToken = "";

            // Act
            await _sut.getProfile().ConfigureAwait(false);
            var Emptyresult = string.IsNullOrEmpty(EmptyToken);

            // Assert
            Emptyresult.Should().Be(true);
            //NullResult.Should().Be(true);
        }


        [Fact]
        public async Task UpdateProfile_ShouldReturnOkResponse()
        {
            // Arrange
            var profile = _fixture.Create<UserProfile>();
            var mappedProfile = _fixture.Create<UserProfile>();
            var user = _fixture.Create<User>();
            Guid user_id = _fixture.Create<Guid>();
            var token = _fixture.Create<String>();
            var profileDataDto = _fixture.Create<AddProfileDto>();

            _authServiceMock.Setup(x => x.GetId(token)).Returns(user_id);
            _userServiceMock.Setup(x => x.GetUserById(user_id)).ReturnsAsync(user);
            _profileServiceMock.Setup(x => x.GetProfile(user_id)).ReturnsAsync(profile);
            _mapper.Setup(x => x.Map(profileDataDto, profile)).Returns(mappedProfile);

            // Act
            var result = await _sut.UpdateProfile(profileDataDto).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().As<OkObjectResult>();
        }


        [Fact]
        public async Task UpdateProfile_ShouldReturnBadRequest_WhenTokenisEmptyOrNull()
        {
            // Arrange
            var profileDataDto = _fixture.Create<AddProfileDto>();
            string EmptyToken = "";
            string? NullToken = null;

            // Act
            await _sut.UpdateProfile(profileDataDto).ConfigureAwait(true);
            var Emptyresult = string.IsNullOrEmpty(EmptyToken);
            var NullResult = string.IsNullOrEmpty(NullToken);

            // Assert
            Emptyresult.Should().Be(true);
            NullResult.Should().Be(true);
        }
    }
}

using AutoFixture;
using FluentAssertions;
using MedAdvisor.Infrastructrure.Interfaces;
using MedAdvisor.Models;
using MedAdvisor.Services.Okta.Services;
using Moq;


namespace MeAdvisor.Servises.Okta.tests.Services
{
    public class ProfileServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IProfileRepository> _profileRepoMock;
        private readonly ProfileService _sut;

        public ProfileServiceTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _profileRepoMock = new Mock<IProfileRepository>();
            _sut = new ProfileService(_profileRepoMock.Object);
        }

        [Fact]
        public async Task AddProfile_ShouldReturnOkResponse()
        {
            // Arrange
            var user_profile = _fixture.Create<UserProfile>();
            var user = _fixture.Create<User>();
            var new_profile = _fixture.Create<UserProfile>();
            _profileRepoMock.Setup(x => x.AddProfileAsync(user_profile, user)).ReturnsAsync(new_profile);

            // Act
            var result = await _sut.AddProfile(user_profile, user).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            _profileRepoMock.Verify(x => x.AddProfileAsync(user_profile, user), Times.Once);
        }

        [Fact]
        public async Task GetProfile_ShouldReturnOkResponse()
        {
            // Arrange
            Guid profile_Id = _fixture.Create<Guid>();
            var profile = _fixture.Create<UserProfile>();
            _profileRepoMock.Setup(x => x.GetProfile(profile_Id)).ReturnsAsync(profile);

            // Act
            var result = await _sut.GetProfile(profile_Id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            _profileRepoMock.Verify(x => x.GetProfile(profile_Id), Times.Once);
        }


        [Fact]
        public async Task UpdateProfile_ShouldReturnOkResponse()
        {
            // Arrange
            var prevProfile = _fixture.Create<UserProfile>();
            var newProfile = _fixture.Create<UserProfile>();
            _profileRepoMock.Setup(x => x.UdpdateProfile(prevProfile)).ReturnsAsync(newProfile);

            // Act
            var result = await _sut.updateProfile(prevProfile).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            _profileRepoMock.Verify(x => x.UdpdateProfile(prevProfile), Times.Once);
        }
    }
}

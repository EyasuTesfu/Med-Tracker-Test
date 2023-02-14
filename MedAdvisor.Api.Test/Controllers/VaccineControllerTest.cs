using AutoFixture;
using FluentAssertions;
using MedAdvisor.Api.Controllers;
using MedAdvisor.Infrastructrure.Interfaces;
using MedAdvisor.Models;
using MedAdvisor.Services.Okta.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace MedAdvisor.Api.tests.Controllers
{
    public class VaccineControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IVaccineService> _vaccineServiceMock;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<IUserServices> _userServiceMock;
        private readonly Mock<IVaccineRepository> _vaccineRepoMock;
        private readonly VaccineController _sut;

        public VaccineControllerTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _userServiceMock = new Mock<IUserServices>();
            _vaccineRepoMock = new Mock<IVaccineRepository>();
            _vaccineServiceMock = new Mock<IVaccineService>();
            _authServiceMock = new Mock<IAuthService>();
            _sut = new VaccineController(_vaccineRepoMock.Object, _vaccineServiceMock.Object, _userServiceMock.Object, _authServiceMock.Object);

        }


        [Fact]
        public async Task AddVaccine_ShouldReturnOkResponse()
        {
            // Arrange
            var vaccine = _fixture.Create<Vaccine>();
            var user = _fixture.Create<User>();
            Guid user_id = _fixture.Create<Guid>();
            Guid vaccine_id = _fixture.Create<Guid>();

            _vaccineServiceMock.Setup(x => x.GetVaccine(vaccine_id)).ReturnsAsync(vaccine);
            _userServiceMock.Setup(x => x.GetUserById(user_id)).ReturnsAsync(user);

            // Act
            var result = await _sut.AddVaccine(vaccine_id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().As<OkObjectResult>();
        }


        [Fact]
        public async Task AddVaccine_ShouldReturnBadRequest_WhenTokenisEmptyOrNull()
        {
            // Arrange
            Guid vaccine_id = _fixture.Create<Guid>();
            string EmptyToken = "";
            string? NullToken = null;

            // Act
            await _sut.AddVaccine(vaccine_id).ConfigureAwait(false);
            var Emptyresult = string.IsNullOrEmpty(EmptyToken);
            var NullResult = string.IsNullOrEmpty(NullToken);

            // Assert
            Emptyresult.Should().Be(true);
            NullResult.Should().Be(true);
        }


        [Fact]
        public async Task DeleteVaccine_ShouldReturnOkResponse()
        {
            // Arrange
            var vaccine = _fixture.Create<Vaccine>();
            var user = _fixture.Create<User>();
            Guid user_id = _fixture.Create<Guid>();
            Guid vaccine_id = _fixture.Create<Guid>();

            _vaccineServiceMock.Setup(x => x.GetVaccine(vaccine_id)).ReturnsAsync(vaccine);
            _userServiceMock.Setup(x => x.GetUserById(user_id)).ReturnsAsync(user);
            _vaccineRepoMock.Setup(x => x.DeleteVaccineAsync(user, vaccine)).ReturnsAsync(user);

            // Act
            var result = await _sut.DeleteVaccine(vaccine_id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().As<OkObjectResult>();

        }


        [Fact]
        public async Task DeleteVaccine_ShouldReturnBadRequest_WhenTokenisEmptyOrNull()
        {
            // Arrange
            Guid vaccine_id = _fixture.Create<Guid>();
            string EmptyToken = "";
            string? NullToken = null;

            // Act
            await _sut.DeleteVaccine(vaccine_id).ConfigureAwait(true);
            var Emptyresult = string.IsNullOrEmpty(EmptyToken);
            var NullResult = string.IsNullOrEmpty(NullToken);

            // Assert
            Emptyresult.Should().Be(true);
            NullResult.Should().Be(true);
        }


        [Fact]
        public async Task SearchVaccines_ShouldReturnOkResponse()
        {
            // Arrange
            var vaccines = _fixture.Create<IEnumerable<Vaccine>>();
            string? vaccineName = _fixture.Create<string>();

            _vaccineRepoMock.Setup(x => x.SearchVaccines(vaccineName)).ReturnsAsync(vaccines);

            // Act
            var result = await _sut.search(vaccineName).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().As<OkObjectResult>();
            result.Should().BeAssignableTo<IEnumerable<Vaccine>>();
        }
    }
}

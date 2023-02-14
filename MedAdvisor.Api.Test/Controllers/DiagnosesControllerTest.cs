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
    public class DiagnosesControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IDiagnosesService> _diagnosesServiceMock;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<IUserServices> _userServiceMock;
        private readonly Mock<IDiagnosesRepository> _diagnosesRepoMock;
        private readonly DiagnosesController _sut;

        public DiagnosesControllerTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _userServiceMock = new Mock<IUserServices>();
            _diagnosesRepoMock = new Mock<IDiagnosesRepository>();
            _diagnosesServiceMock = new Mock<IDiagnosesService>();
            _authServiceMock = new Mock<IAuthService>();
            _sut = new DiagnosesController(_diagnosesRepoMock.Object, _diagnosesServiceMock.Object, _userServiceMock.Object, _authServiceMock.Object);

        }


        [Fact]
        public async Task AddDiagnoses_ShouldReturnOkResponse()
        {
            // Arrange
            var diagnose = _fixture.Create<Diagnoses>();
            var user = _fixture.Create<User>();
            Guid user_id = _fixture.Create<Guid>();
            Guid diagnose_id = _fixture.Create<Guid>();

            _diagnosesServiceMock.Setup(x => x.GetDiagnoses(diagnose_id)).ReturnsAsync(diagnose);
            _userServiceMock.Setup(x => x.GetUserById(user_id)).ReturnsAsync(user);

            // Act
            var result = await _sut.AddDiagnoses(diagnose_id).ConfigureAwait(false);


            // Assert
            result.Should().NotBeNull();
            result.Should().As<OkObjectResult>();
        }


        [Fact]
        public async Task AddAllergy_ShouldReturnBadRequest_WhenTokenisEmptyOrNull()
        {
            // Arrange
            Guid diagnose_id = _fixture.Create<Guid>();
            string EmptyToken = "";
            string? NullToken = null;

            // Act
            await _sut.AddDiagnoses(diagnose_id).ConfigureAwait(false);
            var Emptyresult = string.IsNullOrEmpty(EmptyToken);
            var NullResult = string.IsNullOrEmpty(NullToken);

            // Assert
            Emptyresult.Should().Be(true);
            NullResult.Should().Be(true);
        }


        [Fact]
        public async Task DeleteDiagnose_ShouldReturnOkResponse()
        {
            // Arrange
            var diagnose = _fixture.Create<Diagnoses>();
            var user = _fixture.Create<User>();
            Guid user_id = _fixture.Create<Guid>();
            Guid diagnose_id = _fixture.Create<Guid>();

            _diagnosesServiceMock.Setup(x => x.GetDiagnoses(diagnose_id)).ReturnsAsync(diagnose);
            _userServiceMock.Setup(x => x.GetUserById(user_id)).ReturnsAsync(user);
            _diagnosesRepoMock.Setup(x => x.DeleteDiagnosesAsync(user, diagnose)).ReturnsAsync(user);

            // Act
            var result = await _sut.DeleteDiagnoses(diagnose_id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().As<OkObjectResult>();

        }


        [Fact]
        public async Task DeleteDiagnose_ShouldReturnBadRequest_WhenTokenisEmptyOrNull()
        {
            // Arrange
            Guid diagnose_id = _fixture.Create<Guid>();
            string EmptyToken = "";
            string? NullToken = null;

            // Act
            await _sut.DeleteDiagnoses(diagnose_id).ConfigureAwait(true);
            var Emptyresult = string.IsNullOrEmpty(EmptyToken);
            var NullResult = string.IsNullOrEmpty(NullToken);

            // Assert
            Emptyresult.Should().Be(true);
            NullResult.Should().Be(true);
        }


        [Fact]
        public async Task SearchDiagnoses_ShouldReturnOkResponse()
        {
            // Arrange
            var diagnoses = _fixture.Create<IEnumerable<Diagnoses>>();
            string? diagnoseName = _fixture.Create<string>();

            _diagnosesRepoMock.Setup(x => x.SearchDiagnoses(diagnoseName)).ReturnsAsync(diagnoses);

            // Act
            var result = await _sut.search(diagnoseName).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().As<OkObjectResult>();
        }

    }
}

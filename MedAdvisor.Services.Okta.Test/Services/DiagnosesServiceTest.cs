using AutoFixture;
using FluentAssertions;
using MedAdvisor.DataAccess.MySql.Repositories;
using MedAdvisor.Infrastructrure.Interfaces;
using MedAdvisor.Models;
using MedAdvisor.Services.Okta.Services;
using Moq;

namespace MeAdvisor.Servises.Okta.tests.Services
{
    public class DiagnosesServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IDiagnosesRepository> _diagnosesRepoMock;
        private readonly DiagnosesService _sut;

        public DiagnosesServiceTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _diagnosesRepoMock = new Mock<IDiagnosesRepository>();
            _sut = new DiagnosesService(_diagnosesRepoMock.Object);
        }

        [Fact]
        public async Task GetDiagnoses_ShouldReturnOkResponse()
        {
            // Arrange
            Guid diagnose_Id = _fixture.Create<Guid>();
            var diagnose = _fixture.Create<Diagnoses>();

            _diagnosesRepoMock.Setup(x => x.GetDiagnoses(diagnose_Id)).ReturnsAsync(diagnose);

            // Act
            var result = await _sut.GetDiagnoses(diagnose_Id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            _diagnosesRepoMock.Verify(x => x.GetDiagnoses(diagnose_Id), Times.Once);
        }
    }
}


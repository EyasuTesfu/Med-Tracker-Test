using AutoFixture;
using FluentAssertions;
using MedAdvisor.Infrastructrure.Interfaces;
using MedAdvisor.Models;
using MedAdvisor.Services.Okta.Services;
using Moq;


namespace MeAdvisor.Servises.Okta.tests.Services
{
    public class VaccineServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IVaccineRepository> _vaccineRepoMock;
        private readonly VaccineService _sut;

        public VaccineServiceTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _vaccineRepoMock = new Mock<IVaccineRepository>();
            _sut = new VaccineService(_vaccineRepoMock.Object);
        }

        [Fact]
        public async Task GetVaccine_ShouldReturnOkResponse()
        {
            // Arrange
            Guid vaccine_Id = _fixture.Create<Guid>();
            var vaccine = _fixture.Create<Vaccine>();

            _vaccineRepoMock.Setup(x => x.GetVaccine(vaccine_Id)).ReturnsAsync(vaccine);

            // Act
            var result = await _sut.GetVaccine(vaccine_Id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            _vaccineRepoMock.Verify(x => x.GetVaccine(vaccine_Id), Times.Once);
        }
    }
}


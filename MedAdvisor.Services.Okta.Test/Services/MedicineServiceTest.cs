using AutoFixture;
using FluentAssertions;
using MedAdvisor.Infrastructrure.Interfaces;
using MedAdvisor.Models;
using MedAdvisor.Services.Okta.Services;
using Moq;


namespace MeAdvisor.Servises.Okta.tests.Services
{
    public class MedicineServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<ImedicineRepository> _medicineRepoMock;
        private readonly MedicineService _sut;

        public MedicineServiceTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _medicineRepoMock = new Mock<ImedicineRepository>();
            _sut = new MedicineService(_medicineRepoMock.Object);
        }

        [Fact]
        public async Task GetMedicine_ShouldReturnOkResponse()
        {
            // Arrange
            Guid medicine_Id = _fixture.Create<Guid>();
            var medicine = _fixture.Create<Medicine>();

            _medicineRepoMock.Setup(x => x.GetMedicine(medicine_Id)).ReturnsAsync(medicine);

            // Act
            var result = await _sut.GetMedicine(medicine_Id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            _medicineRepoMock.Verify(x => x.GetMedicine(medicine_Id), Times.Once);
        }
    }
}


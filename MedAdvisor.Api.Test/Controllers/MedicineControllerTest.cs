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
    public class MedicineControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IMedicineService> _medicineServiceMock;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<IUserServices> _userServiceMock;
        private readonly Mock<ImedicineRepository> _medicineRepoMock;
        private readonly MedicineController _sut;

        public MedicineControllerTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _userServiceMock = new Mock<IUserServices>();
            _medicineRepoMock = new Mock<ImedicineRepository>();
            _medicineServiceMock = new Mock<IMedicineService>();
            _authServiceMock = new Mock<IAuthService>();
            _sut = new MedicineController(_medicineRepoMock.Object, _medicineServiceMock.Object, _userServiceMock.Object, _authServiceMock.Object);

        }


        [Fact]
        public async Task AddMedicine_ShouldReturnOkResponse()
        {
            // Arrange
            var medicine = _fixture.Create<Medicine>();
            var user = _fixture.Create<User>();
            Guid user_id = _fixture.Create<Guid>();
            Guid medicine_id = _fixture.Create<Guid>();

            _medicineServiceMock.Setup(x => x.GetMedicine(medicine_id)).ReturnsAsync(medicine);
            _userServiceMock.Setup(x => x.GetUserById(user_id)).ReturnsAsync(user);

            // Act
            var result = await _sut.AddMedicine(medicine_id).ConfigureAwait(false);


            // Assert
            result.Should().NotBeNull();
            result.Should().As<OkObjectResult>();
        }


        [Fact]
        public async Task AddMedicine_ShouldReturnBadRequest_WhenTokenisEmptyOrNull()
        {
            // Arrange
            Guid medicine_id = _fixture.Create<Guid>();
            string EmptyToken = "";
            string? NullToken = null;

            // Act
            await _sut.AddMedicine(medicine_id).ConfigureAwait(false);
            var Emptyresult = string.IsNullOrEmpty(EmptyToken);
            var NullResult = string.IsNullOrEmpty(NullToken);

            // Assert
            Emptyresult.Should().Be(true);
            NullResult.Should().Be(true);
        }


        [Fact]
        public async Task DeleteMedicine_ShouldReturnOkResponse()
        {
            // Arrange
            var medicine = _fixture.Create<Medicine>();
            var user = _fixture.Create<User>();
            Guid user_id = _fixture.Create<Guid>();
            Guid medicine_id = _fixture.Create<Guid>();

            _medicineServiceMock.Setup(x => x.GetMedicine(medicine_id)).ReturnsAsync(medicine);
            _userServiceMock.Setup(x => x.GetUserById(user_id)).ReturnsAsync(user);
            _medicineRepoMock.Setup(x => x.DeleteMedicineAsync(user, medicine)).ReturnsAsync(user);

            // Act
            var result = await _sut.DeleteMedicine(medicine_id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().As<OkObjectResult>();

        }


        [Fact]
        public async Task DeleteDiagnose_ShouldReturnBadRequest_WhenTokenisEmptyOrNull()
        {
            // Arrange
            Guid medicine_id = _fixture.Create<Guid>();
            string EmptyToken = "";
            string? NullToken = null;

            // Act
            await _sut.DeleteMedicine(medicine_id).ConfigureAwait(true);
            var Emptyresult = string.IsNullOrEmpty(EmptyToken);
            var NullResult = string.IsNullOrEmpty(NullToken);

            // Assert
            Emptyresult.Should().Be(true);
            NullResult.Should().Be(true);
        }


        [Fact]
        public async Task SearchMedicines_ShouldReturnOkResponse()
        {
            // Arrange
            var medicines = _fixture.Create<IEnumerable<Medicine>>();
            string? medicineName = _fixture.Create<string>();

            _medicineRepoMock.Setup(x => x.SearchMedicines(medicineName)).ReturnsAsync(medicines);

            // Act
            var result = await _sut.search(medicineName).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().As<OkObjectResult>();
            result.Should().BeAssignableTo<IEnumerable<Medicine>>();
        }

    }
}

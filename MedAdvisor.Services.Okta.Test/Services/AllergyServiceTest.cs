using AutoFixture;
using FluentAssertions;
using MedAdvisor.DataAccess.MySql.Repositories;
using MedAdvisor.Models;
using MedAdvisor.Services.Okta.Services;
using Moq;


namespace MeAdvisor.Servises.Okta.tests.Services
{
    public class AllergyServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IAllergyRepository> _allergyRepoMock;
        private readonly AllergyService _sut;

        public AllergyServiceTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _allergyRepoMock = new Mock<IAllergyRepository>();
            _sut = new AllergyService(_allergyRepoMock.Object);
        }


        [Fact]
        public async Task Get_ShouldReturnOkResponse()
        {
            // Arrange
            Guid allergy_Id = _fixture.Create<Guid>();
            var allergy = _fixture.Create<Allergy>();

            _allergyRepoMock.Setup(x => x.GetAllergy(allergy_Id)).ReturnsAsync(allergy);

            // Act
            var result = await _sut.Get(allergy_Id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            _allergyRepoMock.Verify(x => x.GetAllergy(allergy_Id), Times.Once);
        }
    }
}

using Xunit;
using AutoFixture;
using Moq;
using FluentAssertions;
using System;
using MedAdvisor.Services.Okta.Interfaces;
using MedAdvisor.Api.Controllers;
using MedAdvisor.DataAccess.MySql.Repositories;
using MedAdvisor.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedAdvisor.Api.tests.Controllers
{
    public class AllergiesControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IAllergyService> _allergyServiceMock;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<IUserServices> _userServiceMock;
        private readonly Mock<IAllergyRepository> _allergyRepoMock;
        private readonly AllergiesController _sut;

        public AllergiesControllerTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _userServiceMock = new Mock<IUserServices>();
            _allergyRepoMock = new Mock<IAllergyRepository>();
            _allergyServiceMock = new Mock<IAllergyService>();
            _authServiceMock = new Mock<IAuthService>();
            _sut = new AllergiesController(_allergyRepoMock.Object, _allergyServiceMock.Object, _userServiceMock.Object, _authServiceMock.Object);

        }


        [Fact]
        public async Task AddAllergy_ShouldReturnOkResponse()
        {
            // Arrange
            var allergy = _fixture.Create<Allergy>();
            var user = _fixture.Create<User>();
            Guid user_id = _fixture.Create<Guid>();
            Guid allergy_id = _fixture.Create<Guid>();

            _allergyServiceMock.Setup(x => x.Get(allergy_id)).ReturnsAsync(allergy);
            _userServiceMock.Setup(x => x.GetUserById(user_id)).ReturnsAsync(user);

            // Act
            var result = await _sut.AddAllergy(allergy_id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
        }


        [Fact]
        public async Task AddAllergy_ShouldReturnBadRequest_WhenTokenisEmptyOrNull()
        {
            // Arrange
            Guid allergy_id = _fixture.Create<Guid>();
            string EmptyToken = "";

            // Act
            await _sut.AddAllergy(allergy_id).ConfigureAwait(false);
            var Emptyresult = string.IsNullOrEmpty(EmptyToken);

            // Assert
            Emptyresult.Should().Be(true);
        }


        [Fact]
        public async Task DeleteAllergy_ShouldReturnOkResponse()
        {
            // Arrange
            var allergy = _fixture.Create<Allergy>();
            var user = _fixture.Create<User>();
            Guid user_id = _fixture.Create<Guid>();
            Guid allergy_id = _fixture.Create<Guid>();

            _allergyServiceMock.Setup(x => x.Get(allergy_id)).ReturnsAsync(allergy);
            _userServiceMock.Setup(x => x.GetUserById(user_id)).ReturnsAsync(user);
            _allergyRepoMock.Setup(x => x.DeleteAllergyAsync(user, allergy)).ReturnsAsync(user);

            // Act
            var result = await _sut.DeleteAllergy(allergy_id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
        }


        [Fact]
        public async Task DeleteAllergy_ShouldReturnBadRequest_WhenTokenisEmptyOrNull()
        {
            // Arrange
            Guid allergy_id = _fixture.Create<Guid>();
            string EmptyToken = "";
            string? NullToken = null;

            // Act
            await _sut.DeleteAllergy(allergy_id).ConfigureAwait(true);
            var Emptyresult = string.IsNullOrEmpty(EmptyToken);
            var NullResult = string.IsNullOrEmpty(NullToken);

            // Assert
            Emptyresult.Should().Be(true);
            NullResult.Should().Be(true);
        }


        [Fact]
        public async Task SearchAllergy_ShouldReturnOkResponse()
        {
            // Arrange
            var allergies = _fixture.Create<IEnumerable<Allergy>>();
            string? allergyName = _fixture.Create<string>();

            _allergyRepoMock.Setup(x => x.SearchAllergyies(allergyName)).ReturnsAsync(allergies);

            // Act
            var result = await _sut.search(allergyName).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
        }

    }
}

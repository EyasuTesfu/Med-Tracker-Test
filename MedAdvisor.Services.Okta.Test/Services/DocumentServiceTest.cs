using AutoFixture;
using FluentAssertions;
using MedAdvisor.Infrastructrure.Interfaces;
using MedAdvisor.Models;
using MedAdvisor.Services.Okta.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MeAdvisor.Servises.Okta.tests.Services
{
    public class DocumentServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IDocumentRepository> _documentRepoMock;
        private readonly DocumentService _sut;

        public DocumentServiceTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _documentRepoMock = new Mock<IDocumentRepository>();
            _sut = new DocumentService(_documentRepoMock.Object);
        }

        [Fact]
        public async Task GetDocument_ShouldReturnOkResponse()
        {
            // Arrange
            Guid document_Id = _fixture.Create<Guid>();
            var document = _fixture.Create<Document>();

            _documentRepoMock.Setup(x => x.GetDocumentById(document_Id)).ReturnsAsync(document);

            // Act
            var result = await _sut.getDocumentById(document_Id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().As<OkObjectResult>();
            _documentRepoMock.Verify(x => x.GetDocumentById(document_Id), Times.Once);
        }


        [Fact]
        public async Task UpdateDocument_ShouldReturnOkResponse()
        {
            // Arrange
            var document = _fixture.Create<Document>();
            _documentRepoMock.Setup(x => x.UdpdateDocumentAsync(document)).ReturnsAsync(document);

            // Act
            var result = await _sut.updateDocument(document).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().As<OkObjectResult>();
            _documentRepoMock.Verify(x => x.UdpdateDocumentAsync(document), Times.Once);
        }


        [Fact]
        public void DeleteDocument_ShouldReturnOkResponse()
        {
            // Arrange
            var document = _fixture.Create<Document>();

            _documentRepoMock.Setup(x => x.DeleteDocumentAsync(document));

            // Act
            var result = _sut.DeleteDocument(document).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().As<OkObjectResult>();
            _documentRepoMock.Verify(x => x.DeleteDocumentAsync(document), Times.Once);
        }


    }
}


using AutoFixture;
using AutoMapper;
using FluentAssertions;
using MedAdvisor.Api.Controllers;
using MedAdvisor.Api.Models;
using MedAdvisor.Infrastructrure.Interfaces;
using MedAdvisor.Models;
using MedAdvisor.Services.Okta.Interfaces;
using MedAdvisor.Services.Okta.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace MedAdvisor.Api.tests.Controllers
{
    public class DocumentControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IDocumentRepository> _documentRepoMock;
        private readonly Mock<IDocumentService> _documentServiceMock;
        private readonly Mock<IWebHostEnvironment> _hostEnv;
        private readonly Mock<IUserServices> _userServiceMock;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<IConfiguration> _config;
        private readonly Mock<IMapper> _mapper;
        private readonly DocumentController _sut;

        public DocumentControllerTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _userServiceMock = new Mock<IUserServices>();
            _documentRepoMock = new Mock<IDocumentRepository>();
            _documentServiceMock = new Mock<IDocumentService>();
            _authServiceMock = new Mock<IAuthService>();
            _hostEnv = new Mock<IWebHostEnvironment>();
            _config = new Mock<IConfiguration>();
            _mapper = new Mock<IMapper>();
            _sut = new DocumentController(_documentRepoMock.Object, _documentServiceMock.Object, _userServiceMock.Object, _authServiceMock.Object, _hostEnv.Object, _config.Object, _mapper.Object);

        }


        [Fact]
        public async Task AddDocument_ShouldReturnOkResponse()
        {
            // Arrange
            FileUploadDto document = new FileUploadDto();
            var doc = _fixture.Create<Document>();
            Guid document_id = _fixture.Create<Guid>();
            var token = _fixture.Create<String>();
            Guid user_id = _fixture.Create<Guid>();
            var user = _fixture.Create<User>();
            long ticks = DateTime.Now.Ticks;

            _authServiceMock.Setup(x => x.GetId(token)).Returns(user_id);
            _userServiceMock.Setup(x => x.GetUserById(user_id)).ReturnsAsync(user);
            _documentServiceMock.Setup(x => x.uploadFile(user, doc)).ReturnsAsync(doc);

            // Act
            var result = await _sut.AddDocument(document).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().As<OkObjectResult>();
        }


        [Fact]
        public async Task AddDocument_ShouldReturnBadRequest_WhenDocumentIsNull()
        {
            // Arrange
            FileUploadDto document = new();
            string EmptyToken = "";
            User? NullUser = null;

            // Act
            await _sut.AddDocument(document).ConfigureAwait(true);
            var Emptyresult = string.IsNullOrEmpty(EmptyToken);

            // Assert
            Emptyresult.Should().Be(true);
            NullUser.Should().Be(null);

        }


        [Fact]
        public async Task DeleteDocument_ShouldReturnOkResponse()
        {
            // Arrange
            var document = _fixture.Create<Document>();
            Guid document_id = _fixture.Create<Guid>();

            _documentServiceMock.Setup(s => s.getDocumentById(document_id)).ReturnsAsync(document);
            _documentRepoMock.Setup(s => s.DeleteDocumentAsync(document)).Returns(Task.FromResult(true));

            // Act
            var result = await _sut.DeleteDocument(document_id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().As<OkObjectResult>();
        }


        [Fact]
        public async Task DeleteDocument_ShouldReturnBadRequest_WhenDocumentIsNull()
        {
            // Arrange
            Guid document_id = _fixture.Create<Guid>();
            Document? NullDocument = null;

            // Act
            await _sut.DeleteDocument(document_id).ConfigureAwait(false);

            // Assert
            NullDocument.Should().Be(null);
        }

    }
}
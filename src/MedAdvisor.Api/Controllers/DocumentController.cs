
using MedAdvisor.Infrastructrure.Interfaces;
using MedAdvisor.Services.Okta.Interfaces;
using Microsoft.Extensions.Primitives;
using MedAdvisor.Api.Responses;
using Microsoft.AspNetCore.Mvc;
using MedAdvisor.Api.Models;
using MedAdvisor.Models;
using AutoMapper;


namespace MedAdvisor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentService _documentService;
        private readonly IWebHostEnvironment _hostEnv;
        private readonly IUserServices _userService;
        private readonly IAuthService _AuthService;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public DocumentController(
            IDocumentRepository documentRepository,
            IDocumentService documentService,
            IUserServices userService,
            IAuthService authService,
            IWebHostEnvironment env,
            IConfiguration config,
            IMapper mapper

            )
        {
            _documentRepository = documentRepository;
            _documentService = documentService;
            _AuthService = authService;
            _userService = userService;
            _mapper = mapper;
            _config = config;
            _hostEnv = env;

        }



        [HttpPost]
        [Route("add")]

        public async Task<IActionResult> AddDocument([FromForm] FileUploadDto document)
        {

            Request.Headers.TryGetValue("Authorization", out StringValues token);
            if (String.IsNullOrEmpty(token))
            {
                return BadRequest("un authorized user");
            }
            var User_Id = _AuthService.GetId(token);
            var user = await _userService.GetUserById(User_Id);
            if (user == null)
            {
                return BadRequest(new ErrorResponse(404, "user not found"));
            }
            long ticks = DateTime.Now.Ticks;
            var rootPath = _hostEnv.ContentRootPath;
            var baseUrl = _config.GetValue<string>("Domain:BaseUrl");
            var absolutePath = _documentService.getAbsolutePath(rootPath, document.File.FileName,ticks);
            var dbPath = _documentService.getDbPath(baseUrl, document.File.FileName,ticks);


            if (document.File?.Length == 0)
            {
                return BadRequest(new ErrorResponse(200, "please select file"));
            }

            using (var stream = new FileStream(absolutePath, FileMode.Create))
            {
                 document.File.CopyTo(stream);
            }

            Document new_document = new();
            _mapper.Map(document, new_document);
            new_document.filePath = dbPath;
            var doc =  await _documentService.uploadFile(user, new_document);
            return Ok(doc);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateDocument([FromForm] UpdateDocumentDto document, Guid id )
        {
            var fetched_document = await _documentRepository.GetDocumentById(id);
            if (fetched_document == null)
            {
                return BadRequest(new ErrorResponse(404, "document not found"));
            }
            long ticks = DateTime.Now.Ticks;
            var rootPath = _hostEnv.ContentRootPath;
            var baseUrl = _config.GetValue<string>("Domain:BaseUrl");
            var absolutePath = _documentService.getAbsolutePath(rootPath, document.File.FileName,ticks);
            var dbPath = _documentService.getDbPath(baseUrl, document.File.FileName,ticks);

            if (document.File?.Length == 0)
            {
                return BadRequest(new ErrorResponse(200, "please select file"));
            }
            using (var stream = new FileStream(absolutePath, FileMode.Create))
            {
                document?.File?.CopyTo(stream);
            }

            _mapper.Map(document, fetched_document);
            fetched_document.filePath = dbPath;
            var doc = await _documentService.updateDocument(fetched_document);
            return Ok(doc);

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDocument([FromRoute] Guid id)
        {
            var document = await _documentService.getDocumentById(id);
            if (document == null) {
                return BadRequest(new ErrorResponse(404,"document not found"));
            }
            await _documentRepository.DeleteDocumentAsync(document);
            return Ok(document);
        }

    }
}





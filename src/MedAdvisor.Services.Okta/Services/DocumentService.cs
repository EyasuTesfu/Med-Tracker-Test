

using MedAdvisor.Infrastructrure.Interfaces;
using MedAdvisor.Models;
using MedAdvisor.Services.Okta.Interfaces;

namespace MedAdvisor.Services.Okta.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;     
        }

        public async Task<Document> getDocumentById(Guid id)
        {
            try
            {
                var doc = await  _documentRepository.GetDocumentById(id);
                return doc;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string getAbsolutePath(string rootPath, string filename, long ticks)
        {
            var folderName = Path.Combine("Resources", "Files");
            var pathToSave = Path.Combine(rootPath, folderName);
            var file_name = ticks + "-" + filename;
            var absolutePath = Path.Combine(pathToSave, file_name);
            return absolutePath;
        }

        public string getDbPath(string baseUrl, string filename,long ticks)
        {
            var folderName = Path.Combine("Resources", "Files");
            var file_name = ticks + "-" + filename;
            var fileDbPath = baseUrl + Path.Combine(folderName, file_name);
            return fileDbPath;
        }

        public async Task<Document> updateDocument(Document document)
        {
            try
            {
                await _documentRepository.UdpdateDocumentAsync(document);
                return document;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Document> uploadFile(User user ,Document doc)
        {
            try
            {
             var document = await _documentRepository.AddDocumentAsync(user, doc);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return doc;
        }

        public async Task DeleteDocument(Document doc)
        {
            try
            {
               await _documentRepository.DeleteDocumentAsync(doc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

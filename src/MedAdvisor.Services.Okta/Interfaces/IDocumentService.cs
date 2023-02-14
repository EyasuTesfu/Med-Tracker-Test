

using MedAdvisor.Models;

namespace MedAdvisor.Services.Okta.Interfaces
{
    public interface IDocumentService
    {
        string getAbsolutePath(string rootPath, string fileName,long ticks);
        string getDbPath(string baseUrl, string fileName,long ticks);
        Task<Document> getDocumentById(Guid id);
        Task<Document> updateDocument(Document document);
        Task DeleteDocument(Document document);
        Task<Document> uploadFile(User user ,Document document);
    }
}

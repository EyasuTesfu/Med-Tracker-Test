

using MedAdvisor.Models;

namespace MedAdvisor.Infrastructrure.Interfaces
{
    public interface IDocumentRepository
    {
        Task<Document> AddDocumentAsync(User user, Document document);
        Task DeleteDocumentAsync( Document document);
        Task<Document> GetDocumentById(Guid id);
        Task<Document> UdpdateDocumentAsync(Document document);
    }
}

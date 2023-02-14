
using MedAdvisor.DataAccess.MySql.DataContext;
using MedAdvisor.Infrastructrure.Interfaces;
using Microsoft.EntityFrameworkCore;
using MedAdvisor.Models;

namespace MedAdvisor.Infrastructrure.Repositories
{
    
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext _db;

        public DocumentRepository(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }
        public async Task<Document> AddDocumentAsync(User user, Document document)
        {
            document.UserId = user.UserId;
            user?.Documents?.Add(document);
            await _db.Documents.AddAsync(document);

            await _db.SaveChangesAsync();
            return document;
        }

        public async Task  DeleteDocumentAsync(Document document)
        {
             _db.Documents.Remove(document);
            await _db.SaveChangesAsync();
        }

      
        public async Task<Document> GetDocumentById(Guid id)
        {
            var document = await _db.Documents
           .FirstOrDefaultAsync(a => a.DocumentId == id);
            return document!;
        }

        public async Task<Document> UdpdateDocumentAsync(Document document)
        {
            _db.Documents.Update(document);
            await _db.SaveChangesAsync();
            return document;
        }
    }
}

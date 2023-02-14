

namespace MedAdvisor.Models
{
    public class Document
    {
        public Guid DocumentId { get; set; }
        public String? filePath { get; set; }
        public String? Title { get; set; }
        public String? Catagory { get; set; }
        public string? Description { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}

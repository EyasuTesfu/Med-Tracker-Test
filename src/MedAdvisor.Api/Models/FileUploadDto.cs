namespace MedAdvisor.Api.Models
{
    public class FileUploadDto
    {
        public IFormFile? File { get; set; }
        public String? Title { get; set; }
        public String? Catagory { get; set; }
        public string? Description { get; set; }
    }
}

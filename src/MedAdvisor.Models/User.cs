namespace MedAdvisor.Models
{
  public class User
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public String Email { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public ICollection<Allergy>? Allergies { get; set; }
        public ICollection<Medicine>? Medicines { get; set; }
        public ICollection<Diagnoses>? Diagnoses { get; set; }
        public ICollection<Vaccine>? Vaccines { get; set; }
        public ICollection<Document>? Documents { get; set; }
        public UserProfile? Profile { get; set; }

    }
}


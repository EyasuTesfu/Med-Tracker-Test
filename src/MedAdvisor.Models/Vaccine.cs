

namespace MedAdvisor.Models
{
    public class Vaccine
    {
        public Guid VaccineId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}

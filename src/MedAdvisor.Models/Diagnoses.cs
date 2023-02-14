

namespace MedAdvisor.Models
{
    public class Diagnoses
    {
        public Guid DiagnosesId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}

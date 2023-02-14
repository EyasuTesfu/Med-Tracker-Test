

namespace MedAdvisor.Models
{
    public class Allergy
    {
        public Guid Id  { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public ICollection<User>? Users { get; set; }


    }
}

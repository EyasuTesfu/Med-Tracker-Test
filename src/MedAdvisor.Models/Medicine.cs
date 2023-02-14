

namespace MedAdvisor.Models
{
    public class Medicine
    {
        public Guid MedicineId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public ICollection<User>? Users { get; set; }


    }
}

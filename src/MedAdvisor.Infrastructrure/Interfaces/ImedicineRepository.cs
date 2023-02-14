

using MedAdvisor.Models;

namespace MedAdvisor.Infrastructrure.Interfaces
{
    public interface ImedicineRepository
    {
        Task<IEnumerable<Medicine>> SearchMedicines(string name);
        Task<User> AddMedicineAsync(User user, Medicine medicine);
        Task<User> DeleteMedicineAsync(User user, Medicine allergy);
        Task<Medicine> GetMedicine(Guid id);
    }
}

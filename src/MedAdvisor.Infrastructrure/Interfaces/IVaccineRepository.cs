

using MedAdvisor.Models;

namespace MedAdvisor.Infrastructrure.Interfaces
{
    public interface IVaccineRepository
    {
        Task<IEnumerable<Vaccine>> SearchVaccines(string name);
        Task<User> AddVaccineAsync(User user, Vaccine vaccine);
        Task<User> DeleteVaccineAsync(User user, Vaccine vaccine);
        Task<Vaccine> GetVaccine(Guid id);
    }
}

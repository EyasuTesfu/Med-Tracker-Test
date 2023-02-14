

using MedAdvisor.Models;

namespace MedAdvisor.Services.Okta.Interfaces
{
    public interface IVaccineService
    {
        Task<Vaccine> GetVaccine(Guid id);
        Task<User> AddVaccine(Guid id);
        void DeleteVaccine(Guid id);
    }
}



using MedAdvisor.Models;

namespace MedAdvisor.Services.Okta.Interfaces
{
    public interface IMedicineService
    {
        Task<Medicine> GetMedicine(Guid id);
        Task<User> AddMedicine(Guid id);
        void DeleteMedicine(Guid id);
    }
}

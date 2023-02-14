using MedAdvisor.Models;

namespace MedAdvisor.Services.Okta.Interfaces
{
    public interface IAllergyService
    {
        Task<Allergy> Get(Guid id);
        Task<User> Add(Guid id);
    }
}

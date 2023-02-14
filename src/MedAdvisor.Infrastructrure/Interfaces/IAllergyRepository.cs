

using MedAdvisor.Models;

namespace MedAdvisor.DataAccess.MySql.Repositories
{
    public interface  IAllergyRepository
    {
        Task<IEnumerable<Allergy>> SearchAllergyies(string N);
        Task<User> AddAllergyAsync(User user, Allergy allergy);
        Task<User> DeleteAllergyAsync(User user, Allergy allergy);

        Task<Allergy> GetAllergy(Guid id);
    }
}

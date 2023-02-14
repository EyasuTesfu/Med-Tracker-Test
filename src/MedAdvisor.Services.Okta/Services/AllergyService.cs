using MedAdvisor.DataAccess.MySql.Repositories;
using MedAdvisor.Models;
using MedAdvisor.Services.Okta.Interfaces;

namespace MedAdvisor.Services.Okta.Services
{
    public class AllergyService : IAllergyService
    {
        private readonly IAllergyRepository _allergyRepository;
        public AllergyService(IAllergyRepository allergyRepository )
        {
        _allergyRepository = allergyRepository;
        }
        public async Task<Allergy> Get(Guid id)
        {
            var allergy = await _allergyRepository.GetAllergy(id);
            return allergy;
        }
        public Task<User> Add(Guid id)
        {
            throw new NotImplementedException();
        }

      

    }
}

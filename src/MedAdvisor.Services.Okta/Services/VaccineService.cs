
using MedAdvisor.Infrastructrure.Interfaces;
using MedAdvisor.Models;
using MedAdvisor.Services.Okta.Interfaces;


namespace MedAdvisor.Services.Okta.Services
{
    public class VaccineService : IVaccineService

    {
        private readonly IVaccineRepository _vaccineRepository;

        public VaccineService(IVaccineRepository vaccineRepository)
        {
            _vaccineRepository = vaccineRepository;
        }
        public Task<User> AddVaccine(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteVaccine(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Vaccine> GetVaccine(Guid id)
        {
            var vaccine = await _vaccineRepository.GetVaccine(id);
            return vaccine;
        }
    }
}

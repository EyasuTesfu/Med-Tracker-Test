

using MedAdvisor.Infrastructrure.Interfaces;
using MedAdvisor.Models;
using MedAdvisor.Services.Okta.Interfaces;

namespace MedAdvisor.Services.Okta.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly ImedicineRepository _medicineRepository;

        public MedicineService(ImedicineRepository medrepository)
        {
            _medicineRepository = medrepository;

        }
        public async Task<Medicine> GetMedicine(Guid id)
        {
            var medicine = await _medicineRepository.GetMedicine(id);
            return medicine;
        }
        public Task<User> AddMedicine(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteMedicine(Guid id)
        {
            throw new NotImplementedException();
        }

       
    }
}

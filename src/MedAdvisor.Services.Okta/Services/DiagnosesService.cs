
using MedAdvisor.Infrastructrure.Interfaces;
using MedAdvisor.Services.Okta.Interfaces;
using MedAdvisor.Models;

namespace MedAdvisor.Services.Okta.Services
{
    public class DiagnosesService : IDiagnosesService
    {
        private readonly IDiagnosesRepository _diagnosesRepository;
        public DiagnosesService(IDiagnosesRepository digrepository)
        {
            _diagnosesRepository = digrepository;
        }
        public Task<User> AddDiagnoses(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteDiagnoses(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Diagnoses> GetDiagnoses(Guid id)
        {
            var diagnoses = await _diagnosesRepository.GetDiagnoses(id);
            return diagnoses;
        }
    }
}


using MedAdvisor.Models;
namespace MedAdvisor.Services.Okta.Interfaces
{
    public interface IDiagnosesService
    {
        Task<Diagnoses> GetDiagnoses(Guid id);
        Task<User> AddDiagnoses(Guid id);
        void DeleteDiagnoses(Guid id);
    }
}

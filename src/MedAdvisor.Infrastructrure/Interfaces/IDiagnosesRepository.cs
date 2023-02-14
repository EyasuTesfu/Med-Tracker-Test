

using MedAdvisor.Models;

namespace MedAdvisor.Infrastructrure.Interfaces
{
    public interface IDiagnosesRepository
    {
        Task<IEnumerable<Diagnoses>> SearchDiagnoses(string name);
        Task<User> AddDiagnosesAsync(User user, Diagnoses diagnoses);
        Task<User> DeleteDiagnosesAsync(User user, Diagnoses diagnoses);
        Task<Diagnoses> GetDiagnoses(Guid id);
    }
}

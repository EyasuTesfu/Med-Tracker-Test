
using MedAdvisor.DataAccess.MySql.DataContext;
using MedAdvisor.Infrastructrure.Interfaces;
using MedAdvisor.Models;
using Microsoft.EntityFrameworkCore;

namespace MedAdvisor.Infrastructrure.Repositories
{
    public class DiagnosesRepository : IDiagnosesRepository
    {
        private readonly AppDbContext _db;
        public DiagnosesRepository(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<User> AddDiagnosesAsync(User user, Diagnoses diagnoses)
        {
            user!.Diagnoses?.Add(diagnoses);
            diagnoses?.Users?.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteDiagnosesAsync(User user, Diagnoses diagnoses)
        {
            user!.Diagnoses?.Remove(diagnoses);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<Diagnoses> GetDiagnoses(Guid id)
        {
            var diagnoses = await _db.Diagnosess.Include(a => a.Users)
           .FirstOrDefaultAsync(a => a.DiagnosesId == id);
            return diagnoses!;
        }

        public async Task<IEnumerable<Diagnoses>> SearchDiagnoses(string name)
        {
            var diagnoses = from diagnos in _db.Diagnosess select diagnos;
            diagnoses = _db.Diagnosess.Where(a => a.Name!.Contains(name));
            var diagnoses_list = await diagnoses.ToListAsync();
            return diagnoses_list;
        }
    }
}

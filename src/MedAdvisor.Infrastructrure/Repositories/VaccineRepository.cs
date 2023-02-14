

using MedAdvisor.DataAccess.MySql.DataContext;
using MedAdvisor.Infrastructrure.Interfaces;
using MedAdvisor.Models;
using Microsoft.EntityFrameworkCore;

namespace MedAdvisor.Infrastructrure.Repositories
{
    public class VaccineRepository : IVaccineRepository
    {
        private readonly AppDbContext _db;
        public VaccineRepository(AppDbContext dbContext)
        {
            _db = dbContext;

        }
        public async Task<User> AddVaccineAsync(User user, Vaccine vaccine)
        {
            user?.Vaccines?.Add(vaccine);
            vaccine?.Users?.Add(user!);
            await _db.SaveChangesAsync();
            return user!;
        }

        public async Task<User> DeleteVaccineAsync(User user, Vaccine vaccine)
        {
            user?.Vaccines?.Remove(vaccine);
            await _db.SaveChangesAsync();
            return user!;
        }

        public async Task<Vaccine> GetVaccine(Guid id)
        {
           var vaccine = await _db.Vaccines.Include(a => a.Users)
          .FirstOrDefaultAsync(a => a.VaccineId == id);
          return vaccine!;
        }

        public async Task<IEnumerable<Vaccine>> SearchVaccines(string name)
        {
            var vaccines = from vaccine in _db.Vaccines select vaccine;
            vaccines = _db.Vaccines.Where(a => a.Name!.Contains(name));
            var vaccines_list = await vaccines.ToListAsync();
            return vaccines_list;
        }
    }
}

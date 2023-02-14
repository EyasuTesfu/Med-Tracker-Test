

using MedAdvisor.DataAccess.MySql.DataContext;
using MedAdvisor.Infrastructrure.Interfaces;
using MedAdvisor.Models;
using Microsoft.EntityFrameworkCore;

namespace MedAdvisor.Infrastructrure.Repositories
{
    public class MedicineRepository : ImedicineRepository
    {
        private readonly AppDbContext _db;
        public MedicineRepository(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<Medicine> GetMedicine(Guid id)
        {
            var medicine = await _db.Medicines.Include(a => a.Users)
            .FirstOrDefaultAsync(a => a.MedicineId == id);
            return medicine!;
        }
        public async Task<User> AddMedicineAsync(User user, Medicine medicine)
        {
            user?.Medicines?.Add(medicine);
            medicine?.Users?.Add(user!);
            await _db.SaveChangesAsync();
            return user!;
        }

        public async Task<User> DeleteMedicineAsync(User user, Medicine medicine)
        {
            user?.Medicines?.Remove(medicine);
            await _db.SaveChangesAsync();
            return user!;
        }


        public async Task<IEnumerable<Medicine>> SearchMedicines(string name)
        {
            var medicines = from medicine in _db.Medicines select medicine;
            medicines = _db.Medicines.Where(a => a.Name!.Contains(name));
            var medicines_list = await medicines.ToListAsync();
            return medicines_list;
        }
    }
}

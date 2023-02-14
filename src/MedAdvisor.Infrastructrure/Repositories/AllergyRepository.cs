
using System.Xml.Linq;
using MedAdvisor.DataAccess.MySql.DataContext;
using MedAdvisor.Models;
using Microsoft.EntityFrameworkCore;


namespace MedAdvisor.DataAccess.MySql.Repositories.Allergies
{
    public class AllergyRepository : IAllergyRepository
    {
        private readonly AppDbContext _db;

        public AllergyRepository(AppDbContext dbContext)
        {
            _db = dbContext;

        }

        public async Task<Allergy> GetAllergy(Guid id)
        {
            var allergy = await _db.Allergies.Include(a => a.Users)
           .FirstOrDefaultAsync(a => a.Id == id);
            return allergy!;
        }

        public async Task<User> AddAllergyAsync(User user ,Allergy allergy)
        {
            user?.Allergies?.Add(allergy);
            allergy?.Users?.Add(user!);
            await _db.SaveChangesAsync();
            return user!;

        }


        public async Task<IEnumerable<Allergy>> SearchAllergyies(string name)
        {
            var allergies = from allergy in _db.Allergies select allergy;
            allergies = _db.Allergies.Where(a => a.Name!.Contains(name));
            var allergies_list = await allergies.ToListAsync();
            return allergies_list;
        }

        public async Task<User> DeleteAllergyAsync(User user, Allergy allergy)
        {
            user?.Allergies?.Remove(allergy);
            await _db.SaveChangesAsync();
            return user!;
        }
    }
}

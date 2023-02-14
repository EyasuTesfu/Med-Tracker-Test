
using System.Reflection.Metadata;
using MedAdvisor.DataAccess.MySql.DataContext;
using MedAdvisor.Infrastructrure.Interfaces;
using MedAdvisor.Models;
using Microsoft.EntityFrameworkCore;

namespace MedAdvisor.Infrastructrure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _db;

        public ProfileRepository(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<UserProfile> AddProfileAsync(UserProfile profile, User user)
        {
            profile.UserId = user.UserId;
            user.Profile = profile;
            await _db.UserProfiles.AddAsync(profile);
            await _db.SaveChangesAsync();
            return profile!;
        }

        public async Task<UserProfile> GetProfile(Guid userId)
        {
            var profile = await _db.UserProfiles.FirstOrDefaultAsync(
                           profile => profile.UserId == userId);
       
            return profile!;
        }

        public async Task<UserProfile> UdpdateProfile(UserProfile profile)
        {
            _db.UserProfiles.Update(profile);
            await _db.SaveChangesAsync();
            return profile!;
        }
    }
}

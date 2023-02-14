

using MedAdvisor.Infrastructrure.Interfaces;
using MedAdvisor.Models;
using MedAdvisor.Services.Okta.Interfaces;

namespace MedAdvisor.Services.Okta.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        public async Task<UserProfile> AddProfile(UserProfile profile,User user)
        {
            try
            {
                var newprofile = await _profileRepository.AddProfileAsync(profile,user);
                return newprofile;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserProfile> GetProfile(Guid id)
        {
            try
            {
                var profile = await _profileRepository.GetProfile(id);
                return profile;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserProfile> updateProfile(UserProfile profile)
        {
            try
            {
                var updated = await _profileRepository.UdpdateProfile(profile);
                return updated;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

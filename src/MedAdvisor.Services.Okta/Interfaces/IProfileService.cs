

using MedAdvisor.Models;

namespace MedAdvisor.Services.Okta.Interfaces
{
    public interface IProfileService
    {
        Task<UserProfile> GetProfile(Guid id);
        Task<UserProfile> AddProfile(UserProfile profile, User user);
        Task<UserProfile> updateProfile(UserProfile profile);
    }
}

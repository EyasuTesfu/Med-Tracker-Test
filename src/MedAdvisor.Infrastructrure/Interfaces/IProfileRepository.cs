

using MedAdvisor.Models;

namespace MedAdvisor.Infrastructrure.Interfaces
{
    public interface IProfileRepository
    {
        Task<UserProfile> AddProfileAsync(UserProfile profile,User user);
        Task<UserProfile> UdpdateProfile(UserProfile profile);
        Task<UserProfile> GetProfile(Guid userId);

    }
}

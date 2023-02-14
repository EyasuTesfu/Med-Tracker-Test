using MedAdvisor.Models;

namespace MedAdvisor.Services.Okta.Interfaces
{
    public interface IUserServices
    {
        Task<User> GetUserById(Guid User_Id);
        Task<User> GetUserByEmail(string email);
        Task<User> FetchUserData(string email);
        //Task<User> CreateUser(object user ,byte[] pasHash, byte[] pasSalt);

    }
}

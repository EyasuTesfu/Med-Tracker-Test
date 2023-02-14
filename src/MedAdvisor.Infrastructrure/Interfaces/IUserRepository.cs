
using MedAdvisor.Models;
namespace MedAdvisor.DataAccess.MySql.Repositories.Users
{
   public interface IUserRepository
    {
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByEmail(string email);
        Task<User> AddUserAsync(User user);
        Task<User> FetchUserData(string email);
    }
}

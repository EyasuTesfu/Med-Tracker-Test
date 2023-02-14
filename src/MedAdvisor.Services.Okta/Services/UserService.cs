using MedAdvisor.DataAccess.MySql.Repositories.Users;
using MedAdvisor.Services.Okta.Interfaces;
using MedAdvisor.Models;

namespace MedAdvisor.Services.Okta.Services
{
    public class UserService : IUserServices
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserById(Guid User_Id)
        {
            var user = await _userRepository.GetUserById(User_Id);
            return user;

        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            return user;

        }

        public async Task<User> FetchUserData(string email)
        {
            var user = await _userRepository.FetchUserData(email);
            return user;
        }
    }
}

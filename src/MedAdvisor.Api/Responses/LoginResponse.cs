using MedAdvisor.Models;

namespace MedAdvisor.Api.Responses
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public User user { get; set; }
        public string token { get; set; }
        public LoginResponse(string message, User new_user,string tokens)
        {
            Message = message;
            user = new_user;
            token = tokens;
        }
    }
}

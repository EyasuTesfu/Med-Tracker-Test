using MedAdvisor.Models;

namespace MedAdvisor.Api.Responses
{
    public class RegistrationResponse
    {
        public string Message { get; set; }
        public User user { get; set; }
        public RegistrationResponse(string message, User new_user)
        {
            Message = message;
            user = new_user;
        }
    }
}

using MedAdvisor.Models;

namespace MedAdvisor.Services.Okta.Interfaces
{
    public interface IAuthService
    {
        Guid GetId(string token);

        bool SendEmail(string email, string subject);
        string CreateToken(User user);
        Task<Google.Apis.Auth.GoogleJsonWebSignature.Payload> VerifyGoogleToken(string token);


    }
}


using Google.Apis.Auth;
using MedAdvisor.Services.Okta.Interfaces;
using Microsoft.Extensions.Options;


namespace MedAdvisor.Api.Clients
{
    public class GoogleClient
    {
        private readonly GoogleAuthSettings _googleAuthSettings;
        public GoogleClient(
            IOptionsSnapshot<GoogleAuthSettings> googleAuthSettings
            )
        {
            _googleAuthSettings = googleAuthSettings.Value;

        }

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string token)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _googleAuthSettings.ClientId }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);
            return payload;
        }

    }
}
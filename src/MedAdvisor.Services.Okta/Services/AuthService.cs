using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.Extensions.Options;
using Google.Apis.Auth;
using MedAdvisor.Models;
using System.Security.Claims;
using MedAdvisor.Services.Okta.Interfaces;
using MedAdvisor.Commons.Email;
using System.Net.Mail;

namespace MedAdvisor.Services.Okta.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly MailSettings _mailSettings;
        private readonly GoogleAuthSettings _googleAuthSettings;

        public AuthService(
            IOptionsSnapshot<GoogleAuthSettings> googleAuthSettings,
            IOptions<MailSettings> mailSettings,
            IConfiguration config
            )
        {
            _googleAuthSettings = googleAuthSettings.Value;
            _mailSettings = mailSettings.Value;
            _configuration = config;

        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>{
             new Claim("Id",user.UserId.ToString()),
             new Claim(ClaimTypes.Email, user.Email),
             new Claim(ClaimTypes.Name, user.FullName),
        };

            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Secret").Value);
            var SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
              claims: claims,
              expires: DateTime.Now.AddDays(30),
              signingCredentials: SigningCredentials
          );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }


        public Guid GetId(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Secret").Value);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var id = jwtToken.Claims.First(x => x.Type == "Id").Value;
            var user_id = new Guid(id);

            return user_id;
        }

        public bool SendEmail(string email, string subject)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            //System.Net.ServicePointManager.Expect100Continue = false;
            var From = "gizawag123@gmail.com";
            var Password = "78563412@GZw";
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(From);
            mailMessage.To.Add(new MailAddress("gaxag123@gmail.com"));
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = "reset your passowrd";
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(From, Password);
            client.Host = "smtp.hostinger.in";
            client.EnableSsl = false;
            client.Port = 487;

            try
            {
                client.Send(mailMessage);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
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

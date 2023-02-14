using System.ComponentModel.DataAnnotations;

namespace MedAdvisor.Api.Dtos
{
    public class ExternalLoginDto
    {
        [Required]
        public string AccessToken { get; set; }
    }
}

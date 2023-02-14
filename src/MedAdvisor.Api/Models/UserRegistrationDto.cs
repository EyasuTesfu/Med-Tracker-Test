using System.ComponentModel.DataAnnotations;

namespace MedAdvisor.Api.Dtos
{
    public class UserRegistrationDto
    {
        [Required]
        [StringLength(20, ErrorMessage = "name length must be in between 4 and 20", MinimumLength = 4)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(20, ErrorMessage = "name length must be in between 4 and 20", MinimumLength = 4)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}

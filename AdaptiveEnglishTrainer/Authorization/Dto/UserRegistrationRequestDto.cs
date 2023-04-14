using AdaptiveEnglishTrainer.Authorization.Dto;
using System.ComponentModel.DataAnnotations;

namespace AdaptiveEnglishTrainer.Authorization.Entity
{
    public class UserRegistrationRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Roles Role { get; set; }
    }
}

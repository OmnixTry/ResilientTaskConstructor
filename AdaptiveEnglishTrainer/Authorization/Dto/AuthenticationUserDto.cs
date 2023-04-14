using System.ComponentModel.DataAnnotations;

namespace AdaptiveEnglishTrainer.Authorization.Entity
{
    public class AuthenticationUserDto
    {

        public string Email { get; set; }

        public string Password { get; set; }
    }
}

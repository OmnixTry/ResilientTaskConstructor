using Microsoft.AspNetCore.Identity;

namespace AdaptiveEnglishTrainer.Authorization.Entity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

using AdaptiveEnglishTrainer.Authorization.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdaptiveEnglishTrainer.Authorization.Db
{
    public class AuthenticationContext : IdentityDbContext<User>
    {
        public AuthenticationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}

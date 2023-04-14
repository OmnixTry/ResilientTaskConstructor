using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdaptiveEnglishTrainer.Authorization.Db
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "Teacher",
                    NormalizedName = "TEACHER"
                },
                new IdentityRole
                {
                    Name = "Student",
                    NormalizedName = "STUDENT"
                }
            );
        }
    }
}

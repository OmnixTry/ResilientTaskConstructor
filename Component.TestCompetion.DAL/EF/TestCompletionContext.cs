using Component.TestCompetion.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace Component.TestCompletion.DAL.EF
{
	internal class TestCompletionContext : DbContext
	{
        protected readonly string connectionString;

        DbSet<Result> Results { get; set; }
		DbSet<ResultTask> Tasks { get; set; }
		DbSet<Answer> Answers { get; set; }

        public TestCompletionContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("res");
        }
    }
}

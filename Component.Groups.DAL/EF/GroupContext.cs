using Component.Groups.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace Component.Groups.DAL.EF
{
	internal class GroupContext : DbContext
	{
		DbSet<Group> Groups { get; set; }
		DbSet<GroupStudent> GroupStudents { get; set;}
		DbSet<GroupTest> GroupTests { get; set; }
		DbSet<User> Users { get; set; }

		private readonly string connectionString;

		public GroupContext(string connectionString)
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
			modelBuilder.HasDefaultSchema("grp");
		}
	}
}

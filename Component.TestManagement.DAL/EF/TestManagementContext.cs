using Component.TestManagement.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestManagement.DAL.EF
{
    public class TestManagementContext : DbContext
    {
		protected readonly string connectionString;

		public DbSet<Answer> Answers { get; set; }
        public DbSet<TaskOption> TaskOptions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestTask> TestsTasks { get; set; }
        
        public TestManagementContext(string connectionString)
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
            modelBuilder.HasDefaultSchema("test");

            modelBuilder.Entity<TestTask>().HasOne(p => p.Test).WithMany(b => b.Tasks)
                .HasForeignKey(p => p.TestId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

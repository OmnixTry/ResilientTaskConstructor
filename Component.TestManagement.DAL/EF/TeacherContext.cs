using Component.TestManagement.DAL.Entity;
using Infrastructure.DAL.Contract;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestManagement.DAL.EF
{
	public class TeacherContext : TestManagementContext
	{
		private readonly IUserProvider userProvider;

		public TeacherContext(string connectionString, IUserProvider userProvider) : base(connectionString)
		{
			this.userProvider = userProvider;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Test>().HasQueryFilter(t => t.UserId == userProvider.GetUserId());
		}
	}
}

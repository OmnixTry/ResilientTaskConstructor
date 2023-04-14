using Component.TestManagement.DAL.Contract;
using Component.TestManagement.DAL.EF;
using Component.TestManagement.DAL.Entity;
using Infrastructure.DAL.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestManagement.DAL.Impl
{
	internal class OptionRepository: RepositoryBase<TaskOption, int, TestManagementContext>, IOptionRepository
	{
		private readonly TestManagementContext context;

		public OptionRepository(TestManagementContext dbContext) : base(dbContext)
		{
			this.context = dbContext;
		}

		protected override DbSet<TaskOption> Set => context.TaskOptions;
	}
}

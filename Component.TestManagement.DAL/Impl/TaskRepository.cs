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
	internal class TaskRepository : RepositoryBase<TestTask, int, TestManagementContext>, ITaskRepository
	{
		public TaskRepository(TestManagementContext dbContext) : base(dbContext)
		{
			
		}
	}
}

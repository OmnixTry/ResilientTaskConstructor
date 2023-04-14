using Component.TestCompetion.DAL.Entity;
using Component.TestCompletion.DAL.Contract;
using Component.TestCompletion.DAL.EF;
using Infrastructure.DAL.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestCompletion.DAL.Impl
{
	internal class ResultRepository : RepositoryBase<Result, int, TestCompletionContext>, IResultRepository
	{
		public ResultRepository(TestCompletionContext dbContext) : base(dbContext)
		{
		}

		public Result GetFull(int id)
		{
			return Set.Include(x => x.ResultTasks).
				ThenInclude(x => x.Answers).
				First(x => x.Id == id);
		}
	}
}

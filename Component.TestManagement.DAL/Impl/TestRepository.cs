using Component.TestManagement.DAL.Contract;
using Component.TestManagement.DAL.EF;
using Component.TestManagement.DAL.Entity;
using Infrastructure.DAL.Entity;
using Infrastructure.DAL.Repo;
using Microsoft.EntityFrameworkCore;

namespace Component.TestManagement.DAL.Impl
{
	internal class TestRepository : RepositoryBase<Test, int, TestManagementContext>, ITestRepository
	{
		public TestRepository(TestManagementContext testContext): base(testContext)
		{
		}

		public List<Test> GetFullTests(Filter<Test, int> filter)
		{
			var querry = Set.AsQueryable();

			querry = ApplyFilter(querry, filter);
			querry = querry.Include(t => t.Tasks).ThenInclude(t => t.TaskOptions);

			return querry.ToList();
		}
	}
}

using Component.TestManagement.DAL.Entity;
using Infrastructure.DAL.Entity;
using Infrastructure.DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestManagement.DAL.Contract
{
	public interface ITestRepository: IRepository<Test, int>
	{
		List<Test> GetFullTests(Filter<Test, int> filter);
	}
}

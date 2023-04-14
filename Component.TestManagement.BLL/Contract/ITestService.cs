using Component.TestManagement.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestManagement.BLL.Contract
{
	public interface ITestService
	{
		List<TestDto> GetTestsSmall();
		TestDto GetFullTest(string testName);
		TestDto GetFullTest(int id);
		TestDto GetMysteryTest(int id);
		TestDto Create(TestDto test);
		TestDto Update(TestDto test);
		void Delete(string name);
		bool IsNameUniqie(string name);
	}
}

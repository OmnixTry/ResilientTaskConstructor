using Component.TestManagement.BLL.Contract;
using Component.TestManagement.BLL.Dto;
using Component.TestManagement.DAL.Contract;
using Component.TestManagement.DAL.Entity;
using Infrastructure.DAL.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestManagement.PL.Controllers
{
	[Route("api/[controller]")]
	//[Authorize(Roles = "Teacher")]
	public class DummyManagementController: Controller
	{
		private readonly IRepository<TestTask, int> testRepo;
		private readonly ITestMgmtUnitOfWork testMgmtUnitOfWork;
		private readonly ITestService testService;

		public DummyManagementController(ITestMgmtUnitOfWork testMgmtUnitOfWork, IRepository<TestTask, int> taskRepo, ITestService testService)
		{
			this.testMgmtUnitOfWork = testMgmtUnitOfWork;
			this.testService = testService;
		}
		[HttpGet()]
		public IActionResult GetAll()
		{
			//return Ok(new string[] { "test1", "test2" });
			//return Ok(testMgmtUnitOfWork.TestRepository.GetAll(null, x => x.TestTasks));
			return Ok(testService.GetFullTest("Apple2"));
		}

		[HttpPost()]
		public IActionResult Create([FromBody] TestDto test)
		{
			//return Ok(new string[] { "test1", "test2" });
			//return Ok(testMgmtUnitOfWork.TestRepository.GetAll(null, x => x.TestTasks));
			testService.Create(test);
			return Ok();
		}
	}
}

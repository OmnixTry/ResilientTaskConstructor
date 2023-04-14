using Component.TestManagement.BLL.Contract;
using Component.TestManagement.BLL.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Component.TestManagement.PL.Controllers
{
	[Route("api/[controller]")]
	public class TestsController : Controller
	{
		private readonly ITestService testService;

		public TestsController(ITestService testService)
		{
			this.testService = testService;
		}

		[HttpGet]
		[Authorize(Roles = "Teacher")]
		public IActionResult GetAll()
		{
			return Ok(testService.GetTestsSmall());
		}

		[HttpGet("{testName}")]
		[Authorize(Roles = "Teacher,Student")]
		public IActionResult GetTest(string testName)
		{
			return Ok(testService.GetFullTest(testName));
		}

		[HttpGet("mystery/{id}")]
		[Authorize(Roles = "Student,Teacher")]
		public IActionResult GetMysteryTest(int id)
		{
			return Ok(testService.GetMysteryTest(id));
		}

		[HttpPost]
		[Authorize(Roles = "Teacher")]
		public IActionResult Create([FromBody]TestDto testDto)
		{			
			return Ok(testService.Create(testDto));
		}

		[HttpPut]
		[Authorize(Roles = "Teacher")]
		public IActionResult UpdateTest([FromBody] TestDto testDto)
		{

			return Ok(testService.Update(testDto));
		}

		[HttpDelete("{testName}")]
		[Authorize(Roles = "Teacher")]
		public IActionResult DeleteTest(string testName)
		{
			if (testService.IsNameUniqie(testName))
			{
				return BadRequest("Test with this name doesn't exist");
			}

			testService.Delete(testName);
			return Ok();
		}
	}
}

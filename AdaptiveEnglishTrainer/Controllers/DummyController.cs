using Component.TestManagement.DAL.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdaptiveEnglishTrainer.Controllers
{
	//[Route("api/[controller]")]
	//[Authorize]
	//[ApiController]
	//public class DummyController : ControllerBase
	//{
	//    [Route("teacher")]
	//    [Authorize(Roles = "Teacher")]
	//    public IActionResult GetTeacher()
	//    {
	//        return Ok("TEACHER XD FUNNY TEXT");
	//    }

	//    [Route("student")]
	//    [Authorize(Roles = "Student")]
	//    public IActionResult GetStudent()
	//    {
	//        return Ok("STUDENT XD FUNNY TEXT");
	//    }
	//}

	[Route("api/[controller]")]
	[ApiController]
	public class DummyController : ControllerBase
	{
		private readonly ITestMgmtUnitOfWork unitOfWork;

		public DummyController(ITestMgmtUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		[Route("sql")]
		[HttpGet]
		public IActionResult GetSQL()
		{
			return Ok(unitOfWork.TestRepository.GetAll(x => x.Tasks.First().Question == "What?" || x.Tasks.First().Question == "Where&&", x => x.Tasks));
		}


		[Route("teacher")]
		[Authorize(Roles = "Teacher")]
		[HttpGet]
		public IActionResult GetTeacher()
		{
			return Ok("TEACHER XD FUNNY TEXT");
		}

		[Route("student")]
		[Authorize(Roles = "Student")]
		[HttpGet]
		public IActionResult GetStudent()
		{
			return Ok("STUDENT XD FUNNY TEXT");
		}
	}
}

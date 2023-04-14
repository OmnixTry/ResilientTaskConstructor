using Component.TestCompletion.BLL.Conract;
using Component.TestCompletion.BLL.Dto;
using Infrastructure.DAL.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Component.TestCompletion.PL.Controllers
{
	[Route("api/tests")]
	public class TestCompletionController: Controller
	{
		private readonly ITestCompletionService completionService;
		private readonly IUserProvider userProvider;

		public TestCompletionController(ITestCompletionService completionService, IUserProvider userProvider)
		{
			this.completionService = completionService;
			this.userProvider = userProvider;
		}

		[HttpPost("{testId}/check")]
		public IActionResult CheckTest(int testId, [FromBody]AttemptDto attempt)
		{
			return Ok(completionService.CheckTest(testId, attempt));
		}

		[HttpPost("{testId}/results")]
		public IActionResult SaveTeacherCheck(int testId, [FromBody] AttemptDto attemptDto)
		{
			var userId = userProvider.GetUserId();
			return Ok(completionService.SaveManualCheckResults(attemptDto));
		}

		[HttpGet("{testId}/results/user")]
		public IActionResult GetCompletionResults(int testId, [FromQuery]string userId = null)
		{
			if(userId == null)
			 userId = userProvider.GetUserId();
			return Ok(completionService.GetAttemptsByStudent(testId, userId));
		}

		[HttpGet("{testId}/results")]
		public IActionResult GetTestResults(int testId, [FromQuery]int groupId)
		{
			return Ok(completionService.GetTestResultsByGrou(testId, groupId));
		}

		[HttpGet("/api/results/{resultId}")]
		public IActionResult GetFullResult(int resultId)
		{
			return Ok(completionService.GetFullAttempt(resultId));
		}

	}
}

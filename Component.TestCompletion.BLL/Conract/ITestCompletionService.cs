using Component.TestCompletion.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestCompletion.BLL.Conract
{
	public interface ITestCompletionService
	{
		AttemptDto CheckTest(int testId, AttemptDto attempt);
		List<AttemptDto> GetAttemptsByStudent(int testId, string userId);
		AttemptDto GetFullAttempt(int attemptId);
		StudentResultDto[] GetTestResultsByGrou(int testId, int groupId);
		AttemptDto SaveManualCheckResults(AttemptDto attempt);
	}
}

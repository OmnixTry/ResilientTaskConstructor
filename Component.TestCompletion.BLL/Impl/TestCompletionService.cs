using AutoMapper;
using Component.Groups.BLL.Contract;
using Component.TestCompetion.DAL.Entity;
using Component.TestCompletion.BLL.Conract;
using Component.TestCompletion.BLL.Dto;
using Component.TestCompletion.DAL.Contract;
using Component.TestManagement.BLL.Contract;
using Component.TestManagement.BLL.Dto;
using Component.TestManagement.DAL.Contract;
using Component.TestManagement.DAL.Entity;
using Infrastructure.DAL.Contract;
using Infrastructure.DAL.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace Component.TestCompletion.BLL.Impl
{
	public class TestCompletionService : ITestCompletionService
	{
		private readonly ITestCompletionUnitOfWork completionUnitOfWork;
		private readonly ITestMgmtUnitOfWork mgmtUnitOfWork;
		//private readonly IUserProvider userProvider;
		private readonly ITestService testService;
		private readonly IGroupService groupService;
		private readonly IMapper mapper;
		private readonly IServiceProvider serviceProvider;

		public TestCompletionService(ITestCompletionUnitOfWork testCompletionUnitOfWork,
			ITestMgmtUnitOfWork testMgmtUnitOfWork,
			//IUserProvider userProvider,
			ITestService testService,
			IGroupService groupService,
			IMapper mapper,
			IServiceProvider serviceProvider)
		{
			this.completionUnitOfWork = testCompletionUnitOfWork;
			this.mgmtUnitOfWork = testMgmtUnitOfWork;
			//this.userProvider = userProvider;
			this.testService = testService;
			this.groupService = groupService;
			this.mapper = mapper;
			this.serviceProvider = serviceProvider;
		}

		public List<AttemptDto> GetAttemptsByStudent(int testId, string userId)
		{
			var attempts = completionUnitOfWork.ResultRepository.GetAll(r => r.StudentId == userId && r.TestId == testId);
			var mappedAttempts = mapper.Map<List<AttemptDto>>(attempts);

			FillMaxScores(mappedAttempts);
			
			return mappedAttempts;
		}

		public AttemptDto GetFullAttempt(int attemptId)
		{
			var attempt = completionUnitOfWork.ResultRepository
				.GetFull(attemptId);

			var test = testService.GetFullTest(attempt.TestId);
			var mappedAttempt = mapper.Map<AttemptDto>(attempt);
			foreach (var item in mappedAttempt.Tasks)
			{
				item.Task = test.Tasks.FirstOrDefault(t => t.Id == item.TaskId);
			}

			var options = test.Tasks.SelectMany(t => t.TaskOptions).ToList();
			foreach (var item in mappedAttempt.Tasks.SelectMany(t => t.Answers))
			{
				if(item.TaskOptionId != null)
				{
					item.Option = mapper.Map<OptionDto>(options.FirstOrDefault(a => a.Id == item.TaskOptionId));
				}
			}

			return mappedAttempt;
		}

		public AttemptDto CheckTest(int testId, AttemptDto attempt)
		{
			var test = testService.GetFullTest(testId);

			foreach (var task in attempt.Tasks)
			{
				var corectAnswers = test.Tasks.Where(t => t.Id == task.TaskId).FirstOrDefault();
				CheckTask(task, corectAnswers);
			}
			attempt.Date = DateTime.Now;
			attempt.Score = attempt.Tasks.Sum(t => t.Score);
			var mappedAttempt = mapper.Map<Result>(attempt);
			if(attempt.StudentId == null)
				mappedAttempt.StudentId = serviceProvider.GetService<IUserProvider>().GetUserId();
			mappedAttempt.TestId = testId;
			
			FillHashes(mappedAttempt);

			completionUnitOfWork.ResultRepository.Add(mappedAttempt);
			completionUnitOfWork.Save();

			var saved = mapper.Map<AttemptDto>(mappedAttempt);
			FillMaxScores(new List<AttemptDto>() { saved });
			return saved;
		}

		public AttemptDto SaveManualCheckResults(AttemptDto attempt)
		{
			var mappedAttempt = mapper.Map<Result>(attempt);

			mappedAttempt.Score = attempt.Tasks.Sum(t => t.Score);

			completionUnitOfWork.ResultRepository.Update(mappedAttempt);
			completionUnitOfWork.Save();
			return mapper.Map<AttemptDto>(mappedAttempt);
		}

		public StudentResultDto[] GetTestResultsByGrou(int testId, int groupId)
		{
			var group = groupService.GetFullGroup(groupId);
			var students = group.Students;
			var studentIds = group.Students.Select(s => s.Id);
			var filter = new Filter<Result, int>();
			filter.AddFilter(g => studentIds.Contains(g.StudentId));
			filter.AddFilter(g => g.TestId == testId);
			var answers = completionUnitOfWork.ResultRepository.GetAll(filter);

			var bests = answers.GroupBy(a => a.StudentId).Select(a => new { id = a.Key , max = a.MaxBy(t => t.Score), maxAttempt = a.FirstOrDefault(x => x.Score == a.MaxBy(t => t.Score).Score) }).ToList();
			var result = bests.Select(b => b.maxAttempt).ToArray();
			var mappedAttempts = mapper.Map<AttemptDto[]>(result);

			var studRes = mappedAttempts.Select(a =>
			{
				return new StudentResultDto()
				{
					Attempt = a,
					Student = students.FirstOrDefault(s => s.Id == a.StudentId),
				};
			}).ToArray();

			return studRes;
		}

		private void FillHashes(Result attempt)
		{
			byte code = (byte)Math.Abs(attempt.StudentId.GetHashCode() % 4);
			attempt.Hash = code;
			foreach (var task in attempt.ResultTasks)
			{
				task.Hash = code;
			}

			foreach (var answer in attempt.ResultTasks.SelectMany(t => t.Answers))
			{
				answer.Hash = code;
			}
		}

		private void FillMaxScores(List<AttemptDto> attempts) 
		{
			var testIds = attempts.Select(r => r.TestId).ToArray();
			var scores = GetTestScores(testIds);

			foreach (var attempt in attempts)
			{
				attempt.MaxScore = scores.Find(t => t.TestId == attempt.TestId)?.Score ?? 0;

			}
		}

		private List<TestScoreDto> GetTestScores(params int[] testIds)
		{
			var tests = mgmtUnitOfWork.TestRepository.GetAll(r => testIds.Contains(r.Id), t => t.Tasks).ToList();
			var results = tests.Select(t => new TestScoreDto() { TestId = t.Id, Score = t.Tasks.Sum(task => task.Score) });
			return results.ToList();
		}

		private void CheckTask(TaskDto task, TestTaskDto correctAnswers)
		{
			var correctOptions = correctAnswers.TaskOptions.Where(o => o.Correct ?? false).ToList();
			task.Score = 0;
			switch (correctAnswers.Type)
			{
				case TaskType.MultipleChoice:
					CheckMultipleCouice(task, correctAnswers);
					return;
				case TaskType.OpenBrackets:
					CheckTextQuestions(task, correctAnswers);
					return;
				case TaskType.General:
					CheckTextQuestions(task, correctAnswers);
					return;
				default:
					return;
			}
		}

		private void CheckMultipleCouice(TaskDto task, TestTaskDto correctAnswers)
		{
			var correctOptions = correctAnswers.TaskOptions.Where(o => o.Correct ?? false).Select(o => o.Id).ToList();
			var inputOptions = task.Answers;
			int correct = 0;
			foreach (var item in inputOptions)
			{
				if (correctOptions.Contains(item.TaskOptionId.Value))
				{
					item.Corect = true;
				}
				else
				{
					item.Corect = false;
				}
			}

			if(task.Answers.Any(a => !a.Corect))
			{
				task.Score = 0;
			}
			else {
				task.Score = ((int)(correctAnswers.Score * (float)task.Answers.Where(a => a.Corect).Count() / correctAnswers.TaskOptions.Where(a => a.Correct.Value).Count()));
			}
		}

		private void CheckTextQuestions(TaskDto task, TestTaskDto correctAnswers)
		{
			var correctOptions = correctAnswers.TaskOptions.Where(o => o.Correct ?? false).Select(o => o.Value).ToList();
			var inputOptions = task.Answers;
			foreach (var item in inputOptions)
			{
				if (correctOptions.Any(o => o.ToLower().Contains(item.Value.ToLower())))
				{
					item.Corect = true;
				}
				else
				{
					item.Corect = false;
				}

				if (task.Answers.Any(a => !a.Corect))
				{
					task.Score = 0;
				}
				else
				{
					task.Score = ((int)(correctAnswers.Score * (float)task.Answers.Where(a => a.Corect).Count() / correctAnswers.TaskOptions.Where(a => a.Correct.Value).Count()));
				}
			} 
		}
	}
}

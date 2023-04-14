using AutoMapper;
using Component.TestCompletion.DAL.Contract;
using Component.TestManagement.BLL.Contract;
using Component.TestManagement.BLL.Dto;
using Component.TestManagement.DAL.Contract;
using Component.TestManagement.DAL.Entity;
using Infrastructure.DAL.Contract;
using Infrastructure.DAL.Entity;

namespace Component.TestManagement.BLL.Impl
{
	internal class TestService : ITestService
	{
		private readonly ITestMgmtUnitOfWork unitOfWork;
		private readonly IUserProvider userProvider;
		private readonly IMapper mapper;
		private readonly ITestCompletionUnitOfWork completionUnitOfWork;

		public TestService(ITestMgmtUnitOfWork unitOfWork, IUserProvider userProvider, IMapper mapper, ITestCompletionUnitOfWork completionUnitOfWork)
		{
			this.unitOfWork = unitOfWork;
			this.userProvider = userProvider;
			this.mapper = mapper;
			this.completionUnitOfWork = completionUnitOfWork;
		}

		public List<TestDto> GetTestsSmall()
		{
			var tests = unitOfWork.TestRepository.GetAll(t => t.UserId == userProvider.GetUserId()).ToList();
			var result = mapper.Map<List<TestDto>>(tests);
			foreach (var item in result)
			{
				item.Tasks = null;
			}
			return result;
		}

		public TestDto GetFullTest(string testName)
		{
			var filter = new Filter<Test, int>();
			filter.AddFilter(x => x.UserId == userProvider.GetUserId() && x.Name == testName);

			var test = unitOfWork.TestRepository
				.GetFullTests(filter)
				.FirstOrDefault();

			var result = mapper.Map<TestDto>(test);

			return result;
		}

		public TestDto GetFullTest(int id)
		{
			var filter = new Filter<Test, int>();
			filter.AddFilter(x => x.Id ==id);

			var test = unitOfWork.TestRepository
				.GetFullTests(filter)
				.FirstOrDefault();

			var result = mapper.Map<TestDto>(test);

			return result;
		}

		public TestDto GetMysteryTest(int id)
		{
			var test = GetFullTest(id);

			foreach (var item in test.Tasks.SelectMany(t => t.TaskOptions))
			{
				item.Correct = null;
			}	

			return test;
		}

		public TestDto Create(TestDto test)
		{
			if (!IsNameUniqie(test.Name))
			{
				throw new InvalidOperationException("Can't create test with duplicate name");
			}

			var entity = mapper.Map<Test>(test);
			entity.UserId = userProvider.GetUserId();
			FillAllowMultiple(entity);

			unitOfWork.TestRepository.Add(entity);
			unitOfWork.Save();

			return mapper.Map<TestDto>(entity);
		}



		public TestDto Update(TestDto test)
		{
			var entity = mapper.Map<Test>(test);
			entity.UserId = userProvider.GetUserId();
			FillAllowMultiple(entity);
			unitOfWork.TestRepository.Update(entity);

			var filter = new Filter<Test, int>();
			filter.AddFilter(t => t.Id == test.Id);
			var dbEbtity = unitOfWork.TestRepository.GetFullTests(filter).FirstOrDefault();


			var ids = test.Tasks.Select(t => t.Id).ToArray();
			var tasksToDelete = dbEbtity.Tasks.Where(t => !ids.Contains(t.Id));

			var existantOptions = test.Tasks.SelectMany(t => t.TaskOptions).Select(o => o.Id);

			var optionsToDelete = dbEbtity.Tasks
				.SelectMany(t => t.TaskOptions)
				.Where(o => !existantOptions.Contains(o.Id));

			unitOfWork.TestTaskRepository.DeleteMany(tasksToDelete);
			unitOfWork.TaskOptionRepository.DeleteMany(optionsToDelete);
			unitOfWork.Save();

			return mapper.Map<TestDto>(entity);
		}

		public void Delete(string name)
		{
			var filter = new Filter<Test, int>();
			filter.AddFilter(x => x.Name == name && x.UserId == userProvider.GetUserId());

			var test = unitOfWork.TestRepository.GetFullTests(filter).FirstOrDefault();
			unitOfWork.TestRepository.Delete(test);

			unitOfWork.Save();
		}

		public bool IsNameUniqie(string name)
		{
			var filter = new Filter<Test, int>();
			filter.AddFilter(x => x.UserId == userProvider.GetUserId() && x.Name == name);

			return unitOfWork.TestRepository.GetCount(filter) == 0;
		}

		private List<TestDto> SearchTests(string name, string topic)
		{
			var tests = unitOfWork.TestRepository.GetAll(t => t.UserId == userProvider.GetUserId()).ToList();
			var result = mapper.Map<List<TestDto>>(tests);
			foreach (var item in result)
			{
				item.Tasks = null;
			}
			return result;
		}

		private void FillAllowMultiple(Test test)
		{
			foreach(var task in test.Tasks)
			{
				task.AllowMultiple = task.TaskOptions.Where(o => o.Correct).Count() > 1;
			}
		}
	}
}

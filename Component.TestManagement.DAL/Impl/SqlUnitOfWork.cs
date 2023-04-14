using Component.TestManagement.DAL.Contract;
using Component.TestManagement.DAL.EF;
using Component.TestManagement.DAL.Entity;
using Infrastructure.DAL.Repo;

namespace Component.TestManagement.DAL.Impl
{
	internal class TestMgmtUnitOfWork : UnitOfWorkBase, ITestMgmtUnitOfWork
	{
		public ITestRepository TestRepository { get; private set; }

		public IRepository<TestTask, int> TestTaskRepository { get; private set; }

		public IRepository<TaskOption, int> TaskOptionRepository { get; private set; }

		public IRepository<Answer, int> AnswerRepository { get; private set; }

		private readonly TestManagementContext context;	

		public TestMgmtUnitOfWork(TestManagementContext context,
			ITestRepository testRepository, 
			IRepository<TestTask, int> testTaskRepository, 
			IRepository<TaskOption, int> taskOptionRepository, 
			IRepository<Answer, int> answerRepository) : base(testRepository, testTaskRepository, taskOptionRepository, answerRepository)
		{
			this.context = context;
			TestRepository = testRepository;
			TestTaskRepository = testTaskRepository;
			TaskOptionRepository = taskOptionRepository;
			AnswerRepository = answerRepository;
		}
	}
}

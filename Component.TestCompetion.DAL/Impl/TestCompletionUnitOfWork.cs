using Component.TestCompetion.DAL.Entity;
using Component.TestCompletion.DAL.Contract;
using Infrastructure.DAL.Repo;

namespace Component.TestCompletion.DAL.Impl
{
	internal class TestCompletionUnitOfWork : UnitOfWorkBase, ITestCompletionUnitOfWork
	{
		public IResultRepository ResultRepository { get; }
		public IRepository<ResultTask, int> ResultTaskRepository { get; }
		public IRepository<Answer, int> AnswerRepository { get; }

		public TestCompletionUnitOfWork(IResultRepository resultRepository, 
			IRepository<ResultTask, int> resultTaskRepository, 
			IRepository<Answer, int> answerRepository) : base(resultRepository, resultTaskRepository, answerRepository)
		{
			ResultRepository = resultRepository;
			ResultTaskRepository = resultTaskRepository;
			AnswerRepository = answerRepository;
		}
	}
}

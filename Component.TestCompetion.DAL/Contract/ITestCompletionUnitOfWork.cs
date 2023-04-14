using Component.TestCompetion.DAL.Entity;
using Infrastructure.DAL.Repo;

namespace Component.TestCompletion.DAL.Contract
{
	public interface ITestCompletionUnitOfWork: IUnitOfWork
	{
		IRepository<Answer, int> AnswerRepository { get; }
		IResultRepository ResultRepository { get; }
		IRepository<ResultTask, int> ResultTaskRepository { get; }
	}
}
using Component.TestManagement.DAL.Entity;
using Infrastructure.DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestManagement.DAL.Contract
{
	public interface ITestMgmtUnitOfWork : IUnitOfWork
	{
		ITestRepository TestRepository { get; }
		IRepository<TestTask, int> TestTaskRepository { get; }
		IRepository<TaskOption, int> TaskOptionRepository { get; }
		IRepository<Answer, int> AnswerRepository { get; }
	}
}

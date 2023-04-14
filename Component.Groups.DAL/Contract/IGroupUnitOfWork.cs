using Component.Groups.DAL.Entity;
using Infrastructure.DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Groups.DAL.Contract
{
	public interface IGroupUnitOfWork : IUnitOfWork
	{
		IRepository<Group, int> GroupRepository { get; }
		IRepository<GroupTest, int> GroupTestRepository { get; }
		IRepository<GroupStudent, int> GroupStudentRepository { get; }
		IRepository<User, string> UserRepository { get; }

	}
}

using Component.Groups.DAL.Contract;
using Component.Groups.DAL.Entity;
using Infrastructure.DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Groups.DAL.Impl
{
	internal class GroupUnitOfWork : UnitOfWorkBase, IGroupUnitOfWork
	{
		public IRepository<Group, int> GroupRepository { get; }
		public IRepository<GroupTest, int> GroupTestRepository { get; }
		public IRepository<GroupStudent, int> GroupStudentRepository { get; }
		public IRepository<User, string> UserRepository { get; }

		public GroupUnitOfWork(IRepository<Group, int> groupRepository,
			IRepository<GroupTest, int> groupTestRepository,
			IRepository<GroupStudent, int> groupStudentRepository,
			IRepository<User, string> userRepository)
			: base(groupRepository, groupTestRepository, groupStudentRepository, userRepository)
		{
			GroupRepository = groupRepository;
			GroupTestRepository = groupTestRepository;
			GroupStudentRepository = groupStudentRepository;
			UserRepository = userRepository;
		}
	}
}

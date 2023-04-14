using AutoMapper;
using Component.Groups.BLL.Contract;
using Component.Groups.BLL.Dto;
using Component.Groups.DAL.Contract;
using Component.Groups.DAL.Entity;
using Component.TestManagement.DAL.Contract;
using Infrastructure.DAL.Contract;

namespace Component.Groups.BLL.Impl
{
	public class GroupService : IGroupService
	{
		private readonly IGroupUnitOfWork unitOfWork;
		private readonly IMapper mapper;
		private readonly IUserProvider userProvider;
		private readonly ITestMgmtUnitOfWork testMgmtUnitOfWork;
		const string teacherRole = "Teacher";
		const string studentRole = "Student";

		public GroupService(IGroupUnitOfWork unitOfWork, IMapper mapper, IUserProvider userProvider, ITestMgmtUnitOfWork testMgmtUnitOfWork)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
			this.userProvider = userProvider;
			this.testMgmtUnitOfWork = testMgmtUnitOfWork;
		}

		public List<GroupDto> GetTeacherGroups(string teacherId)
		{
			var groups = unitOfWork.GroupRepository.GetAll(g => g.TeacherId == teacherId);
			var mapped = mapper.Map<List<GroupDto>>(groups);
			return mapped;
		}

		public List<GroupDto> GetStudentGroups(string studentId)
		{
			var stdGroups = unitOfWork.GroupStudentRepository
				.GetAll(g => g.StudentId == studentId)
				.Select(g => g.GroupId).ToArray();

			var groups = unitOfWork.GroupRepository.GetAll(g => stdGroups.Contains(g.Id), g => g.Teacher);
			var mapped = mapper.Map<List<GroupDto>>(groups);
			return mapped;
		}

		public List<GroupDto> GetUserGroups(string userId)
		{			
			if (userProvider.CheckUserRole(teacherRole))
			{
				return GetTeacherGroups(userId);
			}

			if (userProvider.CheckUserRole(studentRole))
			{
				return GetStudentGroups(userId);
			}

			return new List<GroupDto>();
		}
		
		public GroupDto GetGroupWithTests(int groupId)
		{
			var group = unitOfWork.GroupRepository.GetAll(g => g.Id == groupId, g => g.Teacher).FirstOrDefault();
			var testIds = unitOfWork.GroupTestRepository.GetAll(t => t.GroupId == groupId).Select(t => t.TestId);
			var tests = testMgmtUnitOfWork.TestRepository.GetAll(t => testIds.Contains(t.Id));

			group.Tests = null;
			var mappedGroup = mapper.Map<GroupDto>(group);
			mappedGroup.Tests = tests.Select(t => new GroupTestDto { Id = t.Id, Name = t.Name, Topic = t.Topic }).ToList();

			return mappedGroup;
		}

		public GroupDto GetFullGroup(int groupId)
		{
			var group = GetGroupWithTests(groupId);
			var users = unitOfWork.GroupStudentRepository.GetAll(g => g.GroupId == group.Id, g => g.Student).Select(u => u.Student);
			group.Students = mapper.Map<List<GroupUserDto>>(users);
			return group;
		}

		public GroupDto CreateGroupWithStudents(GroupDto group)
		{
			var mappedGroup = MapGroupWithUsers(group);	
			unitOfWork.GroupRepository.Add(mappedGroup);
			unitOfWork.Save();
			if (group.Tests != null && group.Tests.Any())
			{
				var testIds = group.Tests.Select(t => t.Id).ToArray();
				UpdateGroupTests(mappedGroup.Id, testIds);
			}
			group.Id = mappedGroup.Id;
			return group;
		}

		public GroupDto UpdateGroupWithStudents(GroupDto group)
		{
			var mappedGroup = MapGroupWithUsers(group);
			mappedGroup.GroupStudents = null;
			mappedGroup.Tests = null;
			unitOfWork.GroupRepository.Update(mappedGroup);
			unitOfWork.Save(); 
			
			var testIds = group.Tests.Select(t => t.Id).ToArray();
			UpdateGroupTests(mappedGroup.Id, testIds);
			
			var studentId = group.Students.Select(t => t.Id).ToArray();
			UpdateGroupUsers(mappedGroup.Id, studentId);
			

			return group;
		}

		public void AssignTestsToGroup(int groupId, params int[] testId)
		{
			var links = testId.Select(t => new GroupTest() { GroupId = groupId, TestId = t }).ToList();
			foreach (var link in links)
			{
				unitOfWork.GroupTestRepository.Add(link);
			}

			unitOfWork.Save();
		}

		public void AssignUserToGroup(int groupId, params string[] testId)
		{
			var links = testId.Select(t => new GroupStudent() { GroupId = groupId, StudentId = t }).ToList();
			foreach (var link in links)
			{
				unitOfWork.GroupStudentRepository.Add(link);
			}

			unitOfWork.Save();
		}

		public void UpdateGroupTests(int groupId, params int[] testId)
		{
			var prevGroups = unitOfWork.GroupTestRepository
				.GetAll(t => t.GroupId == groupId).ToList();

			int minLength = Math.Min(testId.Length, prevGroups.Count());
			for(int i = 0; i < minLength; i++)
			{
				prevGroups[i].TestId = testId[i];
			}

			unitOfWork.GroupTestRepository
				.UpdateMany(prevGroups);

			if(minLength == testId.Length)
			{
				unitOfWork.GroupTestRepository
					.DeleteMany(prevGroups.Skip(minLength));
			}
			else
			{
				var newTests = testId.Skip(minLength).ToArray();
				AssignTestsToGroup(groupId, newTests);
			}

			unitOfWork.Save();
		}

		public void UpdateGroupUsers(int groupId, params string[] userIds)
		{
			var prevGroups = unitOfWork.GroupStudentRepository
				.GetAll(t => t.GroupId == groupId).ToList();

			int minLength = Math.Min(userIds.Length, prevGroups.Count());
			for (int i = 0; i < minLength; i++)
			{
				prevGroups[i].StudentId = userIds[i];
			}

			unitOfWork.GroupStudentRepository
				.UpdateMany(prevGroups.Take(minLength));

			if (minLength == userIds.Length)
			{
				unitOfWork.GroupStudentRepository
					.DeleteMany(prevGroups.Skip(minLength));
			}
			else
			{
				var newTests = userIds.Skip(minLength).ToArray();
				AssignUserToGroup(groupId, newTests);
			}

			unitOfWork.Save();
		}

		public void Delete(int groupId)
		{
			var group = unitOfWork.GroupRepository.GetAll(g => g.Id == groupId).FirstOrDefault();
			unitOfWork.GroupRepository.Delete(group);
			unitOfWork.Save();
		}

		private Group MapGroupWithUsers(GroupDto group)
		{
			var students = group.Students;
			//group.Students = null;
			//group.Tests = null;

			var mappedGroup = new Group() { Id = group.Id, Name = group.Name, TeacherId = group.TeacherId }; // mapper.Map<Group>(group);

			mappedGroup.TeacherId = userProvider.GetUserId();
			mappedGroup.GroupStudents = MapGroupUsers(group, students);
			//mappedGroup.Tests = null;

			return mappedGroup;
		}

		private List<GroupStudent> MapGroupUsers(GroupDto group, List<GroupUserDto> groupUsers)
		{
			if (group.Id != 0)
			{
				var newIds = groupUsers.Select(x => x.Id).ToArray();
				var users = unitOfWork.GroupStudentRepository.GetAll(g => g.GroupId == group.Id && newIds.Contains(g.StudentId));
				var newUsers = groupUsers.Where(gu => !users.Any(u => u.StudentId == gu.Id)).ToList();
				var newUsersMapped = newUsers.Select(student => new GroupStudent() { StudentId = student.Id })
				.ToList();

				return newUsersMapped.Concat(users).ToList();
			}
			else
			{
				return groupUsers.Select(student => new GroupStudent() { StudentId = student.Id })
				.ToList();
			}			
		}
	}
}
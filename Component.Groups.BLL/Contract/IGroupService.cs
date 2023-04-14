using Component.Groups.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Groups.BLL.Contract
{
	public interface IGroupService
	{
		void AssignTestsToGroup(int groupId, params int[] testId);
		void AssignUserToGroup(int groupId, params string[] testId);
		GroupDto CreateGroupWithStudents(GroupDto group);
		void Delete(int groupId);
		GroupDto GetGroupWithTests(int groupId);
		List<GroupDto> GetStudentGroups(string studentId);
		List<GroupDto> GetTeacherGroups(string teacherId);
		List<GroupDto> GetUserGroups(string userId);
		GroupDto GetFullGroup(int groupId);
		void UpdateGroupTests(int groupId, params int[] testId);
		void UpdateGroupUsers(int groupId, params string[] userIds);
		GroupDto UpdateGroupWithStudents(GroupDto group);
	}
}

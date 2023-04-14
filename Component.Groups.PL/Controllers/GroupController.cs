using Component.Groups.BLL.Contract;
using Component.Groups.BLL.Dto;
using Infrastructure.DAL.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Groups.PL.Controllers
{
	[Route("api/[controller]")]
	public class GroupController : Controller
	{
		private readonly IGroupService groupService;
		private readonly IUserProvider userProvider;

		public GroupController(IGroupService groupService, IUserProvider userProvider)
		{
			this.groupService = groupService;
			this.userProvider = userProvider;
		}

/*		[HttpGet("{userId}")]
		public IActionResult GetGroups(string userId)
		{
			return Ok(groupService.GetUserGroups(userId));
		}*/

		[HttpGet]
		public IActionResult GetCurrentUserGroups()
		{
			string userId = userProvider.GetUserId();
			return Ok(groupService.GetUserGroups(userId));
		}

		[HttpGet("{groupId}")]
		public IActionResult GetGroup(int groupId)
		{
			return Ok(groupService.GetGroupWithTests(groupId));
		}

		[HttpDelete("{groupId}")]
		public IActionResult DeleteGroup(int groupId)
		{
			groupService.Delete(groupId);
			return Ok();
		}

		[HttpGet("{groupId}/full")]
		public IActionResult GetGroupFull(int groupId)
		{
			return Ok(groupService.GetFullGroup(groupId));
		}

		[HttpPost()]
		public IActionResult CreateGroup([FromBody]GroupDto group)
		{
			return Ok(groupService.CreateGroupWithStudents(group));
		}

		[HttpPut()]
		public IActionResult UpdateGroup([FromBody] GroupDto group)
		{			
			return Ok(groupService.UpdateGroupWithStudents(group));
		}

		[HttpPut("{groupId}/tests")]
		public IActionResult UpdateTestToGroup(int groupId, [FromBody]int[] testIds)
		{
			groupService.UpdateGroupTests(groupId, testIds);
			return Ok();
		}
	}
}

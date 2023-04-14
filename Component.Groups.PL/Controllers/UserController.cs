using Component.Groups.BLL.Contract;
using Component.Groups.BLL.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Component.Groups.PL.Controllers
{
	[Route("api/[controller]")]
	public class SearchController : Controller
	{
		private readonly ISearchService userService;

		public SearchController(ISearchService userService)
		{
			this.userService = userService;
		}

		[HttpGet("students")]
		public List<GroupUserDto> SearchUsers([FromQuery] string email)
		{
			return userService.SearchUser(email);
		}

		[HttpGet("tests")]
		public List<GroupTestDto> SearchTests([FromQuery] string name, [FromQuery]string topic)
		{
			return userService.SearchTest(name, topic);
		}
	}
}

//using AdaptiveEnglishTrainer.Authorization.Entity;
using Infrastructure.DAL.Contract;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using AdaptiveEnglishTrainer.Authorization.Entity;

namespace AdaptiveEnglishTrainer.Authorization.Impl
{
	public class UserProvider : IUserProvider
	{
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly UserManager<User> userManager;

		public UserProvider(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
		{
			this.httpContextAccessor = httpContextAccessor;
			this.userManager = userManager;
		}

		public string GetUserId()
		{
			var userName = httpContextAccessor.HttpContext?.User.Identity.Name;
			var x = httpContextAccessor.HttpContext.User;
				var userId = userManager.FindByNameAsync(x.Identity.Name).Result;
			//.GetUserId();// .FindFirst(ClaimTypes.NameIdentifier)?.Value;
			return userId.Id;
		}

		public bool CheckUserRole(string role)
		{
			return httpContextAccessor.HttpContext.User.IsInRole(role);
		}
	}

	public class MockUserProvider : IUserProvider
	{
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly UserManager<User> userManager;

		public MockUserProvider()
		{
		}

		public string GetUserId()
		{
			return "9e4470ba-0de7-4c17-9850-6d96ee25f94c";
		}

		public bool CheckUserRole(string role)
		{
			return "Teacher" == role;
		}
	}
}

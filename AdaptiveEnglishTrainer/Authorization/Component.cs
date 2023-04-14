using AdaptiveEnglishTrainer.Authorization.Impl;
using Infrastructure.DAL.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace AdaptiveEnglishTrainer.Authorization
{
	public static class Component
	{
		public static void RegisterAuthServices(this IServiceCollection serviceDescriptors)
		{
			serviceDescriptors.AddTransient<IUserProvider, UserProvider>();
		}
	}
}

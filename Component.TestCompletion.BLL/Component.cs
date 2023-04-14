using Component.TestCompletion.BLL.Conract;
using Component.TestCompletion.BLL.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Component.TestCompletion.BLL
{
	public static class Component
	{
		public static void RegisterTestCompletionnBll(this IServiceCollection serviceDescriptors)
		{
			serviceDescriptors.AddAutoMapper(typeof(TestCompletionMapperProfile));
			serviceDescriptors.AddScoped<ITestCompletionService, TestCompletionService>();
		}
	}
}
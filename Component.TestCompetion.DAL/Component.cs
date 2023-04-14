using Component.TestCompletion.DAL.Contract;
using Component.TestCompletion.DAL.EF;
using Component.TestCompletion.DAL.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Component.TestCompetion.DAL
{
    public static class Component
    {
        public static void RegisterCompletionDAL(this IServiceCollection serviceDescriptors, string connectionString)
        {
            serviceDescriptors.AddScoped<TestCompletionContext>(s => new TestCompletionContext(connectionString));
            serviceDescriptors.AddTransient<ITestCompletionUnitOfWork, TestCompletionUnitOfWork>(); 
            serviceDescriptors.AddTransient<IResultRepository, ResultRepository>(); 
        }
    }
}
using Component.TestManagement.DAL.Contract;
using Component.TestManagement.DAL.EF;
using Component.TestManagement.DAL.Entity;
using Component.TestManagement.DAL.Impl;
using Infrastructure.DAL.Contract;
using Infrastructure.DAL.Repo;
using Microsoft.Extensions.DependencyInjection;

namespace Component.TestManagement.DAL
{
    public static class Component
    {
        public static void RegisterDAL(this IServiceCollection serviceDescriptors, string connectionString)
		{
            serviceDescriptors.AddTransient<TeacherContext>(s => new TeacherContext(connectionString, s.GetRequiredService<IUserProvider>()));
            serviceDescriptors.AddScoped<TestManagementContext>(s => new TestManagementContext(connectionString));
            serviceDescriptors.AddTransient<ITestMgmtUnitOfWork, TestMgmtUnitOfWork>();
            serviceDescriptors.AddTransient<IRepository<Test, int>, TestRepository>();
            serviceDescriptors.AddTransient<ITestRepository, TestRepository>();
            //serviceDescriptors.AddTransient<ITestMgmtUnitOfWork, SqlUnitOfWork>();
        }
    }
}
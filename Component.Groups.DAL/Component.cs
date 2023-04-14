using Component.Groups.DAL.Contract;
using Component.Groups.DAL.EF;
using Component.Groups.DAL.Impl;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Groups.DAL
{
    public static class Component
    {
        public static void RegisterGroupsDAL(this IServiceCollection serviceDescriptors, string connectionString)
        {
            serviceDescriptors.AddScoped<GroupContext>(s => new GroupContext(connectionString));
            serviceDescriptors.AddScoped<IGroupUnitOfWork, GroupUnitOfWork>();
            /*serviceDescriptors.AddTransient<TeacherContext>(s => new TeacherContext(connectionString, s.GetRequiredService<IUserProvider>()));
            serviceDescriptors.AddScoped<TestManagementContext>(s => new TestManagementContext(connectionString));
            serviceDescriptors.AddTransient<ITestMgmtUnitOfWork, SqlUnitOfWork>();
            serviceDescriptors.AddTransient<IRepository<Test, int>, TestRepository>();
            serviceDescriptors.AddTransient<ITestRepository, TestRepository>();*/
            //serviceDescriptors.AddTransient<ITestMgmtUnitOfWork, SqlUnitOfWork>();
        }
    }
}

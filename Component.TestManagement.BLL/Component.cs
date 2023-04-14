using Component.TestManagement.BLL.Contract;
using Component.TestManagement.BLL.Impl;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestManagement.BLL
{
	public static class Component
	{
        public static void RegisterTestManagementBll(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddAutoMapper(typeof(TestMgmtMapperProfile));
            serviceDescriptors.AddScoped<ITestService, TestService>();
        }
    }
}

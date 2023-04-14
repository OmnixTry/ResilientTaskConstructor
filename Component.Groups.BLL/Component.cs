using Component.Groups.BLL.Contract;
using Component.Groups.BLL.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Component.Groups.BLL
{
    public static class Component
    {
        public static void RegisterGroupsBLL(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddAutoMapper(typeof(GroupMappingProfile));
            serviceDescriptors.AddScoped<IGroupService, GroupService>();
            serviceDescriptors.AddScoped<ISearchService, SearchService>();
        }
    }
}
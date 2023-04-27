using Infrastructure.DAL.Entity;
using Infrastructure.DAL.Repo;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL.Extension
{
	public static class ServicesExtension
	{
		public static void RegisterEfRepositories(this IServiceCollection serviceDescriptors)
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			var types = assemblies.SelectMany(x => x.GetTypes())
				.Where(t => { 
					var attr = t.GetCustomAttributes<EfEntityAttribute>(true); 
					return attr != null && attr.Any(); 
				}).Select(x => new { Type = x, Attributes = x.GetCustomAttributes<EfEntityAttribute>(true) })
				.ToList();
			
			foreach (var item in types)
			{
				var interf = item.Type.GetInterface("IEntity`1");
				var idType = interf.GetGenericArguments().First();

				var interfaceType = typeof(IRepository<,>).MakeGenericType(new[] { item.Type, idType });
				var implementationType = typeof(RepositoryBase<,,>).MakeGenericType(new[] { item.Type, idType, item.Attributes.First().DbContextType });
				serviceDescriptors.AddTransient(interfaceType, implementationType);
			}

			//var t = typeof(int);
			//builder.Services.AddTransient(typeof(IRepository<,>).MakeGenericType(new[] { typeof(TestTask), typeof(int) }), typeof(RepositoryBase<,,>).MakeGenericType(new[] { typeof(TestTask), typeof(int), typeof(TestManagementContext) }));



		}

		public static void RegisterConnectionStrings(this IServiceCollection serviceDescriptors, params string[] strings)
		{
			serviceDescriptors.AddTransient<ConnectionStringProvider>(s => new ConnectionStringProvider(strings));
		}
	}
}

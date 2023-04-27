using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL.Extension
{
	public static class DbContextExtensions
	{
		public static void ApplyConnectionString(this DbContext serviceDescriptors,  string connectionString) { }
	}
}

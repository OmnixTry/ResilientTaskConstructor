using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL
{
	public class EfEntityAttribute: Attribute
	{
		public Type DbContextType { get; }

		public EfEntityAttribute(Type dbContextType)
		{
			DbContextType = dbContextType;
		}
	}
}

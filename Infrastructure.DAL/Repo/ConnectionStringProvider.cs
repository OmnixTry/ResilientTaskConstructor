using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL.Repo
{
	public class ConnectionStringProvider
	{
		private readonly string[] connectionStrings;
		private int currentString = 0;
		public ConnectionStringProvider(params string[] connectionStrings) 
		{
			this.connectionStrings = connectionStrings;
		}

		public string GetConnectionString()
		{
			var conStr = connectionStrings[currentString];
			currentString = (currentString + 1)% connectionStrings.Length;
			return conStr;
		}
	}
}

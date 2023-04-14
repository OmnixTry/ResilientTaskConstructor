using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL.Contract
{
	public interface IUserProvider
	{
		string GetUserId();
		bool CheckUserRole(string role);
	}
}

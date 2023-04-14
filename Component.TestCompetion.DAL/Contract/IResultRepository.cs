using Component.TestCompetion.DAL.Entity;
using Infrastructure.DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestCompletion.DAL.Contract
{
	public interface IResultRepository: IRepository<Result, int>
	{
		Result GetFull(int id);
	}
}

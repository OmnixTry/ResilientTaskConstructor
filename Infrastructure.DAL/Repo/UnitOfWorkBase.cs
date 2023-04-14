using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL.Repo
{
	public class UnitOfWorkBase : IUnitOfWork
	{
		private readonly IRepository[] repositories;

		public UnitOfWorkBase(params IRepository[] repositories)
		{
			this.repositories = repositories;
		}

		public void Dispose()
		{
			foreach (var item in repositories)
			{
				item.Dispose();
			}
		}

		public void Save()
		{
			foreach (var item in repositories)
			{
				item.SaveChanges();
			}
		}
	}
}

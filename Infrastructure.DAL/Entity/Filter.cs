using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL.Entity
{
	public class Filter<TEntity, Tid> where TEntity : IEntity<Tid>
	{
		public List<Tid> Ids { get; private set; }

		public List<Expression<Func<TEntity, object>>> Includes { get; private set; }

		public List<Expression<Func<TEntity, bool>>> Predicates { get; private set; }

		public int Take { get; set; }

		public Filter()
		{
			Ids = new List<Tid>();
			Includes = new List<Expression<Func<TEntity, object>>>();
			Predicates = new List<Expression<Func<TEntity,bool>>>();
		}

		public void Include(Expression<Func<TEntity, object>> include)
		{
			Includes.Add(include);
		}

		public void AddFilter(Expression<Func<TEntity, bool>> filter)
		{
			Predicates.Add(filter);
		}

	}
}

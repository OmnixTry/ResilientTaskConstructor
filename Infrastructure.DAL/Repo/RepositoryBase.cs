using Infrastructure.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.DAL.Repo
{
	public class RepositoryBase<TEntity, Tid, TContext> : IRepository<TEntity, Tid> where TEntity : class, IEntity<Tid> where TContext : DbContext
	{
		protected virtual DbSet<TEntity> Set { get => dbContext.Set<TEntity>(); }

		protected readonly TContext dbContext;

		public RepositoryBase(TContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public virtual void Add(TEntity entity)
		{
			Set.Add(entity);
		}

		public virtual void Delete(TEntity id)
		{
			Set.Remove(id);
		}

		public virtual void DeleteMany(IEnumerable<TEntity> id)
		{
			Set.RemoveRange(id);
		}

		public virtual TEntity Get(Tid id)
		{
			return Set.Find(id);
		}

		public virtual IEnumerable<TEntity> GetAll()
		{
			return Set.ToList();
			}

		public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
		{
			IQueryable<TEntity> querry = Set.AsQueryable();
			querry = ApplyIncludes(querry, includes);
			querry = ApplyPredicates(querry, filter);
			return querry.ToList();
		}

		public virtual IEnumerable<TEntity> GetAll(Filter<TEntity, Tid> filter)
		{
			IQueryable<TEntity> querry = Set.AsQueryable();
			querry = ApplyFilter(querry, filter);
			if(filter.Take >= 0)
			{
				querry.Take(filter.Take);
			}
			return querry.ToList();
		}

		public virtual int GetCount(Filter<TEntity, Tid> filter)
		{
			IQueryable<TEntity> querry = Set.AsQueryable();
			querry = ApplyFilter(querry, filter);
			return querry.Count();
		}

		public virtual void SaveChanges()
		{
			dbContext.SaveChanges();
		}

		public virtual void Update(TEntity entity)
		{
			Set.Update(entity);
			Set.Include(x => x.Id);
		}

		public virtual void UpdateMany(IEnumerable<TEntity> entities)
		{
			Set.UpdateRange(entities);
		}

		public IEnumerable<TEntity> ReadProcedure(string name, params string[] parameters)
		{
			return Set.FromSqlRaw(name, parameters).ToList();
		}

		public void Dispose()
		{
			dbContext?.Dispose();
		}
		protected IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> entities, params Expression<Func<TEntity, object>>[] includes)
		{
			if(includes == null || includes.Length == 0)
			{
				return entities;
			}
			else
			{
				foreach (var item in includes)
				{
					entities = entities.Include(item);
				}

				return entities;
			}
		}

		protected IQueryable<TEntity> ApplyPredicates(IQueryable<TEntity> entities, params Expression<Func<TEntity, bool>>[] filters)
		{
			if (filters == null || filters.Length == 0 || filters.All(f => f == null))
			{
				return entities;
			}
			else
			{
				foreach (var item in filters)
				{
					entities = entities.Where(item);
				}

				return entities;
			}
		}

		protected IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> entities, Filter<TEntity, Tid> filter)
		{
			entities = ApplyIncludes(entities, filter.Includes.ToArray());
			entities = ApplyPredicates(entities, filter.Predicates.ToArray());
			return entities;
		}
	}
}

using Infrastructure.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL.Repo
{
	public interface IRepository : IDisposable
    {
        void SaveChanges();
    }

    public interface IRepository<TEntity, Tid>: IRepository where TEntity : IEntity<Tid>
    {
        TEntity Get(Tid id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes);

        IEnumerable<TEntity> GetAll(Filter<TEntity, Tid> filter);
        
        int GetCount(Filter<TEntity, Tid> filter);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void UpdateMany(IEnumerable<TEntity> entities);

        void Delete(TEntity id);

        void DeleteMany(IEnumerable<TEntity> id);

        IEnumerable<TEntity> ReadProcedure(string name, params string[] parameters);
    }
}

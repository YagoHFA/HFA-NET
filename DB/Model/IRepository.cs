using System.Linq.Expressions;
using System.Net;
using HFA.DB.Model.Interfaces;

namespace HFA;

public interface IRepository<TEntity> : ICrudRepository<TEntity>, IFindRepository<TEntity>, IDeleteRepository<TEntity> where TEntity : class
{
    void AddMany(IEnumerable<TEntity> entities);
    bool Any(Expression<Func<TEntity, bool>> predicate);
    bool Exists(Predicate<TEntity> predicate);
    int Count(Expression<Func<TEntity, bool>> predicate);
}

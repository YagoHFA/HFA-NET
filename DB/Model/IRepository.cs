using System.Linq.Expressions;

namespace HFA;

public interface IRepository<TEntity> where TEntity : class
{

    IQueryable<TEntity>? FindAll();
    IQueryable<TEntity>? FindAll(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity>? FindAll(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>>? orderBy = null);
    TEntity? Find(Expression<Func<TEntity, bool>> predicate);
    void Add(TEntity entity);
    void AddMany(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Delete(TEntity entity);   
    void DeleteMany(Expression<Func<TEntity, bool>> predicate);
    bool Any(Expression<Func<TEntity, bool>> predicate);
    int Count(Expression<Func<TEntity, bool>> predicate);
}

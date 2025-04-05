using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace HFA.DB.Model
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext context;
        protected readonly DbSet<TEntity> dbSet;

        public Repository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public void AddMany(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
            context.SaveChanges();
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Any(predicate);
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Count(predicate);
        }

        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
            context.SaveChanges();
        }

        public void DeleteById(object id)
        {
            TEntity? entity  =  dbSet.Find(id);
            if(entity != null)
                dbSet.Remove(entity);
        }

        public void DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> entities = dbSet.Where(predicate);
            dbSet.RemoveRange(entities);
            context.SaveChanges();
        }

        public TEntity? Find(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity>? FindAll()
        {
            return dbSet.AsQueryable();
        }

        public IQueryable<TEntity>? FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public IQueryable<TEntity>? FindAll(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            var query = dbSet.Where(predicate);
            return orderBy != null ? orderBy(query) : query;
        }

        public TEntity? FindById(params object[] keyValues)
        {
            return dbSet.Find(keyValues);
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
            context.SaveChanges();
        }
    }
}
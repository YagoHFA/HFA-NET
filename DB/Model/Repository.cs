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

        /// <summary>
        /// Add a new entity to the database.
        /// </summary>
        /// <param name="entity">The generic entity to add.</param>
        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Adds multiple entities to the database.
        /// </summary>
        /// <param name="entities">A list containing all entities to be added.</param>
        public void AddMany(IQueryable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        /// <summary>
        /// Verifies whether any entity exists that matches the given filter.
        /// </summary>
        /// <param name="predicate">A filter used to search the database.</param>
        /// <returns>True if at least one entity is found; otherwise, false.</returns>
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
        }

        public void DeleteById(object id)
        {
            TEntity? entity  =  dbSet.Find(id);
            if(entity != null)
            dbSet.Remove(entity);
        }

        public void DeleteMany(IQueryable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public void DeleteWhere(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> entities = dbSet.Where(predicate);
            dbSet.RemoveRange(entities);
        }

        public bool Exists(Predicate<TEntity> predicate)
        {
            return dbSet.ToList().Exists(predicate);
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
        }
    }
}
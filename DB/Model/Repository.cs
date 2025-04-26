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
        }

        public void AddMany(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
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

        public TEntity? FindById(object[] keyValues, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            IQueryable<TEntity> query = dbSet;

            if (includes != null)
                query = includes(query);

            var entityType = context.Model.FindEntityType(typeof(TEntity));
            if (entityType == null)
                throw new InvalidOperationException($"Entity type {typeof(TEntity).Name} not found in model.");

            var keyProps = entityType.FindPrimaryKey()?.Properties;
            if (keyProps == null || keyProps.Count != keyValues.Length)
                throw new ArgumentException("Key values count does not match the number of key properties.");

            var parameter = Expression.Parameter(typeof(TEntity), "e");
            Expression? predicate = null;

            for (int i = 0; i < keyProps.Count; i++)
            {
                var propertyAccess = Expression.Call(
                    typeof(EF),
                    nameof(EF.Property),
                    new[] { typeof(object) },
                    parameter,
                    Expression.Constant(keyProps[i].Name)
                );

                var equals = Expression.Equal(
                    propertyAccess,
                    Expression.Convert(Expression.Constant(keyValues[i]), typeof(object))
                );

                predicate = predicate == null ? equals : Expression.AndAlso(predicate, equals);
            }

            var lambda = Expression.Lambda<Func<TEntity, bool>>(predicate!, parameter);
            return query.FirstOrDefault(lambda);
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }
    }
}
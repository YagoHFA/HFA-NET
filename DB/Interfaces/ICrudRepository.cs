using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HFA.DB.Model.Interfaces
{
    public interface ICrudRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        void Delete(TEntity entity);

        void Update(TEntity entity);
        TEntity? FindById(params object[] keyValues);
        TEntity? FindById(object[] keyValues,  Func<IQueryable<TEntity>, IQueryable<TEntity>> includes);
    }
} 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HFA.DB.Model.Interfaces
{
    public interface IDeleteRepository<TEntity> where TEntity: class
    {
            void DeleteMany(IQueryable<TEntity> entities);

            void DeleteWhere(Expression<Func<TEntity, bool>> predicate);
            
            void DeleteById(Object id);
    }
}
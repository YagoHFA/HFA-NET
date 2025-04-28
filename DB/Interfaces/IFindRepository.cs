using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HFA.DB.Model.Interfaces
{
    public interface IFindRepository<TEntity> where TEntity : class
    {
        TEntity? Find(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity>? FindAll();

        IQueryable<TEntity>? FindAll(Expression<Func<TEntity, bool>> predicate);
        
        IQueryable<TEntity>? FindAll(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>>? orderBy = null);
    }
}
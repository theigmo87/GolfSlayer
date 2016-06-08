using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNet.Identity.EntityFramework;
using Repositories.Database;
using Repositories.Entities;

namespace Repositories
{
    public interface IRepositoryBase<TEntity, in TKey> where TEntity : class, IEntity
    {
        IDbSet<TEntity> Items { get; }
        DataContext Context { get; }
        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity GetByID(TKey id, params Expression<Func<TEntity, object>>[] includeProperties);
        void InsertOrUpdate(TEntity entity);
        void HardDelete(TKey id);
        void Save();
        void ExecuteCommand(string sql, params object[] parameters);
        void Dispose();
    }
    public abstract class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class, IEntity
    {
        private readonly DataContext dataContext;

        protected RepositoryBase(DataContext context)
        {
            dataContext = context;
        }

        protected RepositoryBase()
        {
            dataContext = new DataContext();
        }
        public virtual IDbSet<TEntity> Items
        {
            get { return dataContext.Set<TEntity>(); }
        }
        public virtual DataContext Context
        {
            get { return dataContext; }
        }
        public virtual IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>
                (dataContext.Set<TEntity>(), (current, expression) => current.Include(expression)).AsEnumerable<TEntity>();
        }
        public virtual TEntity GetByID(TKey id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            if (includeProperties.Any())
            {
                var set = includeProperties.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(dataContext.Set<TEntity>(), (current, expression) => current.Include(expression));
                ParameterExpression param = Expression.Parameter(typeof(TEntity), "s");
                Expression left = Expression.Property(param, typeof(TEntity).GetProperty("ID"));
                Expression right = Expression.Constant(id);
                Expression expr = Expression.Equal(left, right);

                return set.SingleOrDefault<TEntity>(Expression.Lambda<Func<TEntity, bool>>(expr, param));
            }
            return dataContext.Set<TEntity>().Find(id);
        }
        public virtual void InsertOrUpdate(TEntity item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var p = typeof(TEntity).GetProperty("ID");

            if (p != null)
            {
                if (item.ID != default(int))
                {
                    dataContext.Entry(item).State = EntityState.Modified;
                }
                else
                {
                    Items.Add(item);
                }
            }
        }
        public virtual void HardDelete(TKey id)
        {
            var entity = GetByID(id);
            Items.Remove(entity);
        }
        public virtual void Save()
        {
            dataContext.SaveChanges();
        }
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (this.dataContext != null)
            {
                this.dataContext.Dispose();
            }
        }
        public void ExecuteCommand(string sql, params object[] parameters)
        {
            this.dataContext.ExecuteCommand(sql, parameters);
        }

        #region Private Helpers
        /// <summary>
        /// Returns expression to use in expression trees, like where statements. For example query.Where(GetExpression("IsDeleted", typeof(boolean), false));
        /// </summary>
        /// <param name="propertyName">The name of the property. Either boolean or a nulleable typ</param>
        private Expression<Func<TEntity, bool>> GetExpression(string propertyName, object value)
        {
            var param = Expression.Parameter(typeof(TEntity));
            var actualValueExpression = Expression.Property(param, propertyName);

            var lambda = Expression.Lambda<Func<TEntity, bool>>(
                Expression.Equal(actualValueExpression,
                    Expression.Constant(value)),
                param);

            return lambda;
        }
        #endregion
    }


}

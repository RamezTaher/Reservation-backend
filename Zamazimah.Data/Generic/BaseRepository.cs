using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Zamazimah.Data.Context;
using Zamazimah.Generic.Models;
using Zamazimah.Models;

namespace Zamazimah.Data.Generic.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ZamazimahContext context;
        protected DbSet<TEntity> dbSet;

        public BaseRepository(ZamazimahContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter).AsNoTracking();
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty).AsNoTracking();
            }

            if (orderBy != null)
            {
                return orderBy(query).AsNoTracking();
            }
            else
            {
                return query;
            }
        }

        public IEnumerable<TEntity> GetWithTracking(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>,
    IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }


        public void Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            dbSet.Add(entity);
        }

        public void InsertMultiple(List<TEntity> entities)
        {
            if (entities != null || entities.Count() > 0)
            {
                dbSet.AddRange(entities);
            }
        }


        public void Update(TEntity entity)
        {
            var dbEntityEntry = context.Entry(entity);
            context.Update(entity);
        }

        public void UpdateMultiple(List<TEntity> entities)
        {
            if (entities != null || entities.Count() > 0)
            {
                dbSet.UpdateRange(entities);
            }
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (context.Entry(entity).State == EntityState.Detached)
            {
                context.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public ResultApiModel<IEnumerable<T>> Paginate<T>(IQueryable<T> query, int page = 1, int take = 30)
        {
            var result = new ResultApiModel<IEnumerable<T>>();
            var total = query.Count();
            if (take != -1)
            {
                query = query.Skip(Math.Max(0, page - 1) * take);
                if (take != 0)
                {
                    query = query.Take(take);
                }
            }
            var items = query.ToList();
            result.Data = items;
            result.TotalCount = total;
            if (take > 0)
            {
                result.PageCount = (total + take - 1) / take;
            }
            result.Success = true;
            result.CurrentPage = page;
            return result;
        }

        public bool IsExist(Expression<Func<TEntity, bool>> filter)
        {
            return dbSet.Any(filter);
        }

        public void RemoveMany(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entity");
            }
            dbSet.RemoveRange(entities);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }
    }
    public interface IRepository<TEntity>
    {
        TEntity GetById(object id);
        void Insert(TEntity entity);
        void InsertMultiple(List<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void UpdateMultiple(List<TEntity> entities);
        int SaveChanges();
        ResultApiModel<IEnumerable<T>> Paginate<T>(IQueryable<T> query, int page = 1, int take = 30);
        bool IsExist(Expression<Func<TEntity, bool>> filter);
        void RemoveMany(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        IEnumerable<TEntity> GetWithTracking(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>,
    IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        IEnumerable<TEntity> GetAll();
    }
}
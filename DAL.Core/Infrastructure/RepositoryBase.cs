using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    public abstract class RepositoryBase<T, idT> : IRepository<T, idT> where T : class where idT : class
    {
        #region Properties
        private DIContext dataContext;
        private DbSet<T> dbSet;

        protected DIContext DbContext
        {
            get { return dataContext; }
        }
        #endregion
        protected RepositoryBase() { }
        protected RepositoryBase(DIContext context)
        {
            dataContext = context;
            Console.WriteLine(typeof(T));
            dbSet = DbContext.Set<T>();
        }

        #region Implementation
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
            dataContext.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
            dataContext.SaveChanges();
        }

        public virtual void Delete(idT id)
        {
            var ent = dbSet.Find(id);
            dbSet.Remove(ent);
            dataContext.SaveChanges();
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
            dataContext.SaveChanges();
        }

        public virtual T GetById(idT id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }

        #endregion

    }
}

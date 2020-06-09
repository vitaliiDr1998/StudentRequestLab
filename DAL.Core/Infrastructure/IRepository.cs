using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace  DAL.Infrastructure
{
    public interface IRepository<inT,idT> where inT : class
    {
        
        void Add(inT entity);
        
        void Update(inT entity);
        
        void Delete(idT id);
        void Delete(Expression<Func<inT, bool>> where);
        
        inT GetById(idT id);
        
        inT Get(Expression<Func<inT, bool>> where);
        
        IEnumerable<inT> GetAll();
        
        IEnumerable<inT> GetMany(Expression<Func<inT, bool>> where);
    }
}

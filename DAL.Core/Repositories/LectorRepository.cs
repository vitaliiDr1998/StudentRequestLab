using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Infrastructure;
using DAL.Entities;

namespace DAL.Repositories
{
    public class LectorRepository : RepositoryBase<Lector, string>
    {
        public LectorRepository() { }
        public LectorRepository(DIContext context)
            : base(context) { }
       
    }
}

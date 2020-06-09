using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        public DIContext dbContext { get; }

        public DbFactory(DIContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}

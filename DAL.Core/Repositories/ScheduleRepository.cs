using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Infrastructure;
using DAL.Entities;

namespace DAL.Repositories
{
    public class ScheduleRepository : RepositoryBase<Schedule, string>
    {
        public ScheduleRepository() { }
        public ScheduleRepository(DIContext context)
            : base(context) { }
    }
}

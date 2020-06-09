using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    public interface IUnitOfWork<id>
    {
        IRepository<Consultation, id> Consultation { get; }
        IRepository<Schedule, id> Schedule { get; }
        IRepository<Student, id> Student { get; }
        IRepository<Lector, id> Lector { get; }
        void Save();
    }
}

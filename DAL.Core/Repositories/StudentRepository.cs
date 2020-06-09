using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Infrastructure;
using DAL.Entities;

namespace DAL.Repositories
{
    public class StudentRepository : RepositoryBase<Student, string>
    {
        public StudentRepository() { }
        public StudentRepository(DIContext context)
            : base(context) { }
    }
}

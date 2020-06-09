using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Infrastructure;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ConsultationRepository : RepositoryBase<Consultation, string>
    {
        public ConsultationRepository() { }
        public ConsultationRepository(DIContext context)
            : base(context) { }

        public override Consultation GetById(string id)
        {
            var intId = int.Parse(id);
            var res = DbContext.ConsultationSet
                .Include(x => x.Lector)
                .Include(x => x.Student)
            .FirstOrDefault(x => x.Id == intId);
            return res;
        }
        public override IEnumerable<Consultation> GetAll()
        {
            var res = DbContext.ConsultationSet
                .Include(x => x.Lector)
                .Include(x => x.Student).ToList();
            return res;
        }
    }
}

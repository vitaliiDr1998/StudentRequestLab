using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        virtual public List<Consultation> Consultations { get; set; }
    }
}

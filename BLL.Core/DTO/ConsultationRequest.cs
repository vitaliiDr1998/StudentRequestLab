using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class ConsultationRequest
    {
        public int LectorId { get; set; }
        public int StudentId { get; set; }
        public string Topic { get; set; }
        public double Duration { get; set; }
        public DateTime Date { get; set; }
    }
}

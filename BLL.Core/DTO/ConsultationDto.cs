using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Core.Entities
{
    public class ConsultationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Duration { get; set; }
        public string Topic { get; set; }
        public bool IsApproved { get; set; }
        public bool IsVisitedByStudent { get; set; }
        public bool IsMissed { get; set; }
        virtual public Student Student { get; set; }
        virtual public Lector Lector { get; set; }
    }
}

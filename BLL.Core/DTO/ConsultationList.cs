using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Core.Entities
{
    public class ConsultationList
    {
        public Dictionary<int, List<ConsultationDto>> Consultations { get; set; }
    }
}

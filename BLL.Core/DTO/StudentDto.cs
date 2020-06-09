using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Core.DTO
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Priority { get; set; }
    }
}

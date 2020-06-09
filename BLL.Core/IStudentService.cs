using BLL.Core.DTO;
using BLL.Core.Entities;
using BLL.Entities;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Core
{
    public interface IStudentService
    {
        List<StudentDto> GetAll();
        void Add(StudentDto student);
        ConsultationList GetAllConsultations();
        bool CreateConsultation(ConsultationRequest consultationRequest);
    }
}

using BLL.Core.DTO;
using BLL.Core.Entities;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Core
{
    public interface ILectorService
    {
        List<LectorDto> GetAll();
        void Add(LectorDto lector);
        void MarkConsultationAsVisitedByStudent(string LectorId, string ConsultationId);
        void MarkConsultationAsMissedByStudent(string LectorId, string ConsultationId);
        void MarkConsultationAsApproved(string LectorId, string ConsultationId);
        List<ConsultationDto> GetConsultationOnTheDay(string LectorId, DateTime dateTime);
    }
}

using BLL.Core.DTO;
using BLL.Core.Entities;
using DAL.Entities;
using DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Core
{
    public class LectorService : ILectorService
    {
        IUnitOfWork<string> unitOfWork;

        public LectorService(IUnitOfWork<string> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Add(LectorDto lector)
        {
            if (lector == null)
                throw new ArgumentNullException(nameof(lector));
            if (string.IsNullOrEmpty(lector.Name))
                throw new ArgumentException("Name should be filled", nameof(lector.Name));
            this.unitOfWork.Lector.Add(new Lector
            {
                Name = lector.Name,
                Surname = lector.Surname
            });
        }
        public List<LectorDto> GetAll()
        {
            var lectors = new List<LectorDto>();
            foreach (var lector in this.unitOfWork.Lector.GetAll().ToList())
            {
                lectors.Add(new LectorDto
                {
                    Id = lector.Id,
                    Name = lector.Name,
                    Surname = lector.Surname
                });

            }
            return lectors;
        }

        public void MarkConsultationAsVisitedByStudent(string LectorId, string ConsultationId)
        {
            if (string.IsNullOrEmpty(LectorId))
                throw new ArgumentNullException(nameof(LectorId), "Must be filled");
            if (string.IsNullOrEmpty(ConsultationId))
                throw new ArgumentNullException(nameof(ConsultationId), "Must be filled");
            var cons = unitOfWork.Consultation.GetById(ConsultationId.ToString());
            if (cons.Lector.Id.ToString() != LectorId)
                throw new InvalidOperationException("Lector doesn't have this consultation");
            cons.IsVisitedByStudent = true;
            unitOfWork.Consultation.Update(cons);
        }

        public void MarkConsultationAsMissedByStudent(string LectorId, string ConsultationId)
        {
            if (string.IsNullOrEmpty(LectorId))
                throw new ArgumentNullException(nameof(LectorId), "Must be filled");
            if (string.IsNullOrEmpty(ConsultationId))
                throw new ArgumentNullException(nameof(ConsultationId), "Must be filled");
            var cons = unitOfWork.Consultation.GetById(ConsultationId.ToString());
            if (cons.Lector.Id.ToString() != LectorId)
                throw new InvalidOperationException("Lector doesn't have this consultation");
            if (cons.IsMissed)
                return;
            cons.IsMissed = true;
            cons.Student.Priority--;
            unitOfWork.Consultation.Update(cons);
        }

        public void MarkConsultationAsApproved(string LectorId, string ConsultationId)
        {
            if (string.IsNullOrEmpty(LectorId))
                throw new ArgumentNullException(nameof(LectorId), "Must be filled");
            if (string.IsNullOrEmpty(ConsultationId))
                throw new ArgumentNullException(nameof(ConsultationId), "Must be filled");
            var cons = unitOfWork.Consultation.GetById(ConsultationId.ToString());
            if (cons.Lector.Id.ToString() != LectorId)
                throw new InvalidOperationException("Lector doesn't have this consultation");
            cons.IsApproved = true;
            unitOfWork.Consultation.Update(cons);
        }

        public List<ConsultationDto> GetConsultationOnTheDay(string LectorId, DateTime dateTime)
        {
            var cons = unitOfWork.Consultation.GetAll().Where(x =>
            x.Lector.Id.ToString() == LectorId &&
            x.Date.DayOfYear == dateTime.DayOfYear &&
            x.Date.Year == dateTime.Year).ToList();
            var result = new List<ConsultationDto>();
            foreach (var consultation in cons)
            {
                result.Add(new ConsultationDto
                {
                    Id = consultation.Id,
                    IsApproved = consultation.IsApproved,
                    Topic = consultation.Topic,
                    Date = consultation.Date,
                    Duration = consultation.Duration,
                    IsMissed = consultation.IsMissed,
                    IsVisitedByStudent = consultation.IsVisitedByStudent
                });
            }
            return result;
        }
    }
}

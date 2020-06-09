using BLL.Core.DTO;
using BLL.Core.Entities;
using BLL.Entities;
using DAL.Entities;
using DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Core
{
    public class StudentService : IStudentService
    {
        IUnitOfWork<string> unitOfWork;

        public StudentService(IUnitOfWork<string> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Add(StudentDto student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            if (string.IsNullOrEmpty(student.Name))
                throw new ArgumentException("Name should be filled", nameof(student.Name));
            this.unitOfWork.Student.Add(new Student
            {
                Name = student.Name,
                Surname = student.Surname
            });
        }
        public List<StudentDto> GetAll()
        {
            var students = new List<StudentDto>();
            foreach (var student in this.unitOfWork.Student.GetAll().ToList())
            {
                students.Add(new StudentDto
                {
                    Id = student.Id,
                    Name = student.Name,
                    Surname = student.Surname,
                    Priority = student.Priority
                });

            }
            return students;
        }

        public bool CreateConsultation(ConsultationRequest consultationRequest)
        {
            var student = unitOfWork.Student.GetById(consultationRequest.StudentId.ToString())
                ?? throw new NullReferenceException($"Student with id = {consultationRequest.StudentId} is missing in system");

            var lector = unitOfWork.Lector.GetById(consultationRequest.LectorId.ToString())
                ?? throw new NullReferenceException($"Lector with id = {consultationRequest.LectorId} is missing in system");
            var cons = new Consultation
            {
                Duration = consultationRequest.Duration,
                Date = consultationRequest.Date,
                Topic = consultationRequest.Topic,
                Student = student,
                Lector = lector
            };
            unitOfWork.Consultation.Add(cons);
            return true;
        }

        public ConsultationList GetAllConsultations()
        {
            var cons = unitOfWork.Consultation.GetAll();
            var res = new ConsultationList();
            res.Consultations = new Dictionary<int, List<ConsultationDto>>();
            var groups = cons.GroupBy(x => x.Student.Id);
            foreach (var studentSet in groups)
            {
                var studentProccecedClaims = new List<ConsultationDto>();
                foreach (var consultation in studentSet)
                {
                    studentProccecedClaims.Add(new ConsultationDto
                    {
                        Id = consultation.Id,
                        IsApproved = consultation.IsApproved,
                        Topic = consultation.Topic,
                        Date = consultation.Date,
                        Duration = consultation.Duration,
                        IsMissed = consultation.IsMissed,
                        IsVisitedByStudent = consultation.IsVisitedByStudent,
                        Lector = consultation.Lector
                    });
                }
                res.Consultations.Add(studentSet.Key, studentProccecedClaims);
            }
            return res;
        }
    }
}

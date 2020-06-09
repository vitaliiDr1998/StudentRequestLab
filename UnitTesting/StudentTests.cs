using BLL.Core;
using BLL.Core.DTO;
using BLL.Entities;
using DAL.Entities;
using Moq;
using System;
using Xunit;

namespace UnitTesting
{
    public class StudentTests : BaseTest
    {
        StudentService studentService;

        public StudentTests()
        {
            this.studentService = new StudentService(Db);
        }

        [Fact]
        public void CreateConsultation_StudentDoesntExist_NullReferenceException()
        {
            
            const int WrongStudentId = 1;
            Mock.Get(lcRep).Setup(x => x.GetById("1")).Returns(new Lector());
            ConsultationRequest consultationRequest = new ConsultationRequest
            {
                LectorId = 1,
                StudentId = WrongStudentId,
                Date = DateTime.Now,
                Duration = 2,
                Topic = "Math"
            };


            Assert.Throws<NullReferenceException>(() => { studentService.CreateConsultation(consultationRequest); });
        }

        [Fact]
        public void CreateConsultation_LectorDoesntExist_NullReferenceException()
        {
            
            const int WrongLectorId = 1;
            Mock.Get(stRep).Setup(x => x.GetById("1")).Returns(new Student());
            ConsultationRequest consultationRequest = new ConsultationRequest
            {
                LectorId = WrongLectorId,
                StudentId = 1,
                Date = DateTime.Now,
                Duration = 2,
                Topic = "Math"
            };

            Assert.Throws<NullReferenceException>(() => { studentService.CreateConsultation(consultationRequest); });
        }

        [Fact]
        public void CreateConsultation_LectorStudentExist_HappyPath()
        {
            
            Mock.Get(lcRep).Setup(x => x.GetById("1")).Returns(new Lector());
            Mock.Get(stRep).Setup(x => x.GetById("1")).Returns(new Student());
            ConsultationRequest consultationRequest = new ConsultationRequest
            {
                LectorId = 1,
                StudentId = 1,
                Date = DateTime.Now,
                Duration = 2,
                Topic = "Math"
            };

            
            var result = studentService.CreateConsultation(consultationRequest);

            
            Assert.True(result);
        }

        [Fact]
        public void Add_StudentNull_NullArgumentException()
        {
            
            StudentDto student = null;


            Assert.Throws<ArgumentNullException>(() => { studentService.Add(student); });
        }

        [Fact]
        public void Add_StudentNameIsNull_ArgumentException()
        {
            
            StudentDto student = new StudentDto();

            Assert.Throws<ArgumentException>(() => { studentService.Add(student); });
        }

        [Fact]
        public void Add_StudentNameIsEmpty_ArgumentException()
        {
            
            StudentDto student = new StudentDto
            {
                Name = ""
            };


            Assert.Throws<ArgumentException>(() => { studentService.Add(student); });
        }
    }
}

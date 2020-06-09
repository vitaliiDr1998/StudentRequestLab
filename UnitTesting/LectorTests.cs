using BLL.Core;
using BLL.Entities;
using DAL.Entities;
using Moq;
using System;
using Xunit;

namespace UnitTesting
{
    public class LectorTests : BaseTest
    {
        LectorService lectorService;

        public LectorTests()
        {
            this.lectorService = new LectorService(Db);
        }

        [Fact]
        public void MarkConsultationAsVisitedByStudent_LectorIdEmpty_ArgumentNullException()
        {
            
            var consultationId = "1";
            var lectorId = "";

           

            
            Assert.Throws<ArgumentNullException>(() => { lectorService.MarkConsultationAsVisitedByStudent(lectorId, consultationId); });
        }

        [Fact]
        public void MarkConsultationAsVisitedByStudent_ConsultationIdEmpty_ArgumentNullException()
        {
            
            var consultationId = "";
            var lectorId = "1";

            

            
            Assert.Throws<ArgumentNullException>(() => { lectorService.MarkConsultationAsVisitedByStudent(lectorId, consultationId); });
        }

        [Fact]
        public void MarkConsultationAsMissedByStudent_LectorIdEmpty_ArgumentNullException()
        {
            
            var consultationId = "1";
            var lectorId = "";

            

            
            Assert.Throws<ArgumentNullException>(() => { lectorService.MarkConsultationAsMissedByStudent(lectorId, consultationId); });
        }

        [Fact]
        public void MarkConsultationAsMissedByStudent_ConsultationIdEmpty_ArgumentNullException()
        {
            
            var consultationId = "";
            var lectorId = "1";

        
            Assert.Throws<ArgumentNullException>(() => { lectorService.MarkConsultationAsMissedByStudent(lectorId, consultationId); });
        }

        [Fact]
        public void MarkConsultationAsApproved_LectorIdEmpty_ArgumentNullException()
        {
            
            var consultationId = "1";
            var lectorId = "";

       
            Assert.Throws<ArgumentNullException>(() => { lectorService.MarkConsultationAsApproved(lectorId, consultationId); });
        }

        [Fact]
        public void MarkConsultationAsApproved_ConsultationIdEmpty_ArgumentNullException()
        {
            
            var consultationId = "";
            var lectorId = "1";

     
            Assert.Throws<ArgumentNullException>(() => { lectorService.MarkConsultationAsApproved(lectorId, consultationId); });
        }

        [Fact]
        public void MarkConsultationAsApproved_ConsultationUpdateCalled_HappyPath()
        {
            
            var consultationId = 1;
            var lectorId = 1;
            var consultation = new Consultation
            {
                Lector = new Lector
                {
                    Id = lectorId
                }
            };

            Mock.Get(csRep).Setup(x => x.GetById(It.IsAny<string>())).Returns(consultation);

            
            lectorService.MarkConsultationAsApproved(lectorId.ToString(), consultationId.ToString());
            
            Mock.Get(csRep).Verify(x => x.Update(It.IsAny<Consultation>()));
        }

        [Fact]
        public void MarkConsultationAsMissedByStudent_ConsultationUpdateCalled_HappyPath()
        {
            
            var consultationId = 1;
            var lectorId = 1;
            var consultation = new Consultation
            {
                Lector = new Lector
                {
                    Id = lectorId
                },
                Student = new Student()
            };

            Mock.Get(csRep).Setup(x => x.GetById(It.IsAny<string>())).Returns(consultation);

            
            lectorService.MarkConsultationAsMissedByStudent(lectorId.ToString(), consultationId.ToString());
            
            Mock.Get(csRep).Verify(x => x.Update(It.IsAny<Consultation>()));
        }

        [Fact]
        public void MarkConsultationAsVisitedByStudent_ConsultationUpdateCalled_HappyPath()
        {
            
            var consultationId = 1;
            var lectorId = 1;
            var consultation = new Consultation
            {
                Lector = new Lector
                {
                    Id = lectorId
                }
            };

            Mock.Get(csRep).Setup(x => x.GetById(It.IsAny<string>())).Returns(consultation);

            
            lectorService.MarkConsultationAsVisitedByStudent(lectorId.ToString(), consultationId.ToString());
            
            Mock.Get(csRep).Verify(x => x.Update(It.IsAny<Consultation>()));
        }

        [Fact]
        public void MarkConsultationAsVisitedByStudent_LectorDoesntHavethisConsultation_InvalidOperationException()
        {
            
            var consultationId = 1;
            var lectorId = 1;
            var wrongLectorId = 2;
            var consultation = new Consultation
            {
                Lector = new Lector
                {
                    Id = lectorId
                }
            };

            Mock.Get(csRep).Setup(x => x.GetById(It.IsAny<string>())).Returns(consultation);


            Assert.Throws<InvalidOperationException>(
                () => { lectorService.MarkConsultationAsVisitedByStudent(wrongLectorId.ToString(), consultationId.ToString()); });
        }

        [Fact]
        public void MarkConsultationAsApproved_LectorDoesntHavethisConsultation_InvalidOperationException()
        {
            
            var consultationId = 1;
            var lectorId = 1;
            var wrongLectorId = 2;
            var consultation = new Consultation
            {
                Lector = new Lector
                {
                    Id = lectorId
                }
            };

            Mock.Get(csRep).Setup(x => x.GetById(It.IsAny<string>())).Returns(consultation);


            Assert.Throws<InvalidOperationException>(
                () => { lectorService.MarkConsultationAsApproved(wrongLectorId.ToString(), consultationId.ToString()); });
        }

        [Fact]
        public void MarkConsultationAsMissedByStudent_LectorDoesntHavethisConsultation_InvalidOperationException()
        {
            
            var consultationId = 1;
            var lectorId = 1;
            var wrongLectorId = 2;
            var consultation = new Consultation
            {
                Lector = new Lector
                {
                    Id = lectorId
                }
            };

            Mock.Get(csRep).Setup(x => x.GetById(It.IsAny<string>())).Returns(consultation);


            Assert.Throws<InvalidOperationException>(
                () => { lectorService.MarkConsultationAsMissedByStudent(wrongLectorId.ToString(), consultationId.ToString()); });
        }
    }
}

using DAL;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTesting
{
    public class BaseTest
    {
        public IRepository<Student, string> stRep = Mock.Of<StudentRepository>();
        public IRepository<Lector, string> lcRep = Mock.Of<LectorRepository>();
        public IRepository<Consultation, string> csRep = Mock.Of<ConsultationRepository>();
        public IUnitOfWork<string> Db;

        public BaseTest()
        {
            
            Db = Mock.Of<IUnitOfWork<string>>(x => x.Lector == lcRep &&
            x.Student == stRep &&
            x.Consultation == csRep);
        }
    }
}

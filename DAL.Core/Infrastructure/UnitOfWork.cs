using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork<string>
    {
        private ConsultationRepository consultationRepository;
        private ScheduleRepository scheduleRepository;
        private StudentRepository studentRepository;
        private LectorRepository lectorRepository;
        private DIContext db;

        public UnitOfWork(DIContext db)
        {
            this.db = db;
        }

        public IRepository<Consultation, string> Consultation
        {
            get
            {
                if (consultationRepository == null)
                    consultationRepository = new ConsultationRepository(db);
                return consultationRepository;
            }
        }

        public IRepository<Schedule, string> Schedule
        {
            get
            {
                if (scheduleRepository == null)
                    scheduleRepository = new ScheduleRepository(db);
                return scheduleRepository;
            }
        }
        public IRepository<Student, string> Student
        {
            get
            {
                if (studentRepository == null)
                    studentRepository = new StudentRepository(db);
                return studentRepository;
            }
        }
        
        public IRepository<Lector, string> Lector
        {
            get
            {
                if (lectorRepository == null)
                    lectorRepository = new LectorRepository(db);
                return lectorRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

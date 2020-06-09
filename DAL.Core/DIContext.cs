
using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DIContext : IdentityDbContext
    {
        public DIContext(DbContextOptions<DIContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Lector> LectorSet { get; set; }
        public DbSet<Consultation> ConsultationSet { get; set; }
        public DbSet<Schedule> ScheduleSet { get; set; }
        public DbSet<Student> StudentSet { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}

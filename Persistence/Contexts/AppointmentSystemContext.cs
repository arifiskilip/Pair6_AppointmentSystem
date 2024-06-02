using Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Core.Security.Entitites;
using Core.Domain;
using System.Reflection;
using System.Security.Cryptography;

namespace Persistence.Contexts
{
    public class AppointmentSystemContext : DbContext
    {
        public AppointmentSystemContext(DbContextOptions options) : base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //    base.OnModelCreating(modelBuilder);
        //    // IEntityConfigurations refarnsı sahip olan tüm assembly (class) oku ve al.


          
          


        //    // Seed Data

        //    //    var salt1 = CreateSalt();
        //    //    var salt2 = CreateSalt();
        //    //    modelBuilder.Entity<User>().HasData(
        //    //    new User
        //    //    {
        //    //        Id = 1,
        //    //        FirstName = "John",
        //    //        LastName = "Doe",
        //    //        BirthDate = new DateTime(1980, 1, 1),
        //    //        IdentityNumber = "12345678901",
        //    //        PhoneNumber = "5380575722",
        //    //        Email = "john.doe@example.com",
        //    //        PasswordHash = GetPasswordHash("123456", salt1),
        //    //        PasswordSalt = salt1
        //    //    },
        //    //    new User
        //    //    {
        //    //        Id = 2,
        //    //        FirstName = "Jane",
        //    //        LastName = "Doe",
        //    //        BirthDate = new DateTime(1985, 2, 2),
        //    //        IdentityNumber = "12345678902",
        //    //        PhoneNumber = "5380575724",
        //    //        Email = "jane.doe@example.com",
        //    //        PasswordHash = GetPasswordHash("123456", salt2),
        //    //        PasswordSalt = salt2

        //    //    }
        //    //);


        //    //    modelBuilder.Entity<Title>().HasData(
        //    //        new Title { Id = 1, Name = "Dr." },
        //    //        new Title { Id = 2, Name = "Prof." }
        //    //    );

        //    //    modelBuilder.Entity<Branch>().HasData(
        //    //        new Branch { Id = 1, Name = "Cardiology" },
        //    //        new Branch { Id = 2, Name = "Neurology" }
        //    //    );

        //    //    modelBuilder.Entity<Doctor>().HasData(
        //    //        new Doctor { Id = 1, UserId = 1, TitleId = 1, BranchId = 1 },
        //    //        new Doctor { Id = 2, UserId = 2, TitleId = 2, BranchId = 2 }
        //    //    );

        //    //    modelBuilder.Entity<AppointmentStatus>().HasData(
        //    //        new AppointmentStatus { Id = 1, Name = "Scheduled" },
        //    //        new AppointmentStatus { Id = 2, Name = "Completed" }
        //    //    );

        //    //    modelBuilder.Entity<DoctorSchedule>().HasData(
        //    //        new DoctorSchedule { Id = 1, DoctorId = 1, Day = new DateTime(2024, 6, 2), StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(18, 0, 0), PatientInterval = new TimeSpan(0, 30, 0) }
        //    //    );

        //    //    modelBuilder.Entity<Patient>().HasData(
        //    //        new Patient { Id = 1,UserId=1, BloodType = "A+" }
        //    //    );

        //    //    modelBuilder.Entity<Appointment>().HasData(
        //    //        new Appointment { Id = 1, PatientId = 1, AppointmentIntervalId = 1, FeedbackId = null, ReportId = null }
        //    //    );

        //    //    modelBuilder.Entity<AppointmentInterval>().HasData(
        //    //        new AppointmentInterval { Id = 1, DoctorId = 1, Day = new DateTime(2024, 6, 2), IntervalStart = new TimeSpan(13, 0, 0), IntervalEnd = new TimeSpan(13, 30, 0), AppointmentStatusId = 1 }
        //    //    );

        //    //    modelBuilder.Entity<Feedback>().HasData(
        //    //        new Feedback { Id = 1, PatientId = 1, Description = "Good service", AppointmentId = 1, Status = true }
        //    //    );

        //    //    modelBuilder.Entity<Report>().HasData(
        //    //        new Report { Id = 1, AppointmentId = 1, Description = "Annual checkup", ReportFile = "report.pdf" }
        //    //    );

        //    //    modelBuilder.Entity<Domain.Entities.OperationClaim>().HasData(
        //    //        new Domain.Entities.OperationClaim { Id = 1, Name = "Admin" },
        //    //        new Domain.Entities.OperationClaim { Id = 2, Name = "User" }
        //    //    );

        //    //    modelBuilder.Entity<Domain.Entities.UserOperationClaim>().HasData(
        //    //        new Domain.Entities.UserOperationClaim { Id = 1, UserId = 1, OperationClaimId = 1 },
        //    //        new Domain.Entities.UserOperationClaim { Id = 2, UserId = 2, OperationClaimId = 2 }
        //    //    );


        //}
        //private static byte[] GetPasswordHash(string password, byte[] salt)
        //{
        //    using (var hmac = new HMACSHA512(salt))
        //    {
        //        return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //    }
        //}

        //private static byte[] CreateSalt()
        //{
        //    using (var rng = new RNGCryptoServiceProvider())
        //    {
        //        var salt = new byte[16];
        //        rng.GetBytes(salt);
        //        return salt;
        //    }
        //}

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<Entity<int>>();
            foreach (var entity in entities)
            {
                if(entity.State == EntityState.Modified)
                {
                   entity.Entity.UpdatedDate = DateTime.UtcNow;

                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentInterval> AppointmentIntervals { get; set; }
        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Domain.Entities.OperationClaim> OperationClaims { get; set; }
        public DbSet<Domain.Entities.UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<User> Users { get; set; }

      
    }
}

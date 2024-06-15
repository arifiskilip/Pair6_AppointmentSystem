using Core.Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UserOperationClaim = Domain.Entities.UserOperationClaim;

namespace Persistence.Contexts
{
    public class AppointmentSystemContext : DbContext
    {
        public AppointmentSystemContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentInterval> AppointmentIntervals { get; set; }
        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Domain.Entities.OperationClaim> OperationClaims { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Domain.Entities.UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public DbSet<CodeType> CodeTypes { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<BloodType> BloodTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TPT Configuration
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Doctor>().ToTable("Doctors");
            modelBuilder.Entity<Patient>().ToTable("Patients");

            //Base configurations
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Title)
                .WithMany(t => t.Doctors)
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Branch)
                .WithMany(b => b.Doctors)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Patient)
                .WithMany(p => p.Feedbacks)
                .HasForeignKey(f => f.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Appointment)
                .WithOne(a => a.Feedback)
                .HasForeignKey<Feedback>(f => f.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Appointment)
                .WithOne(a => a.Report)
                .HasForeignKey<Report>(r => r.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserOperationClaim>()
                .HasOne(uoc => uoc.User)
                .WithMany(u => u.UserOperationClaims)
                .HasForeignKey(uoc => uoc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserOperationClaim>()
                .HasOne(uoc => uoc.OperationClaim)
                .WithMany(oc => oc.UserOperationClaims)
                .HasForeignKey(uoc => uoc.OperationClaimId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
              .HasOne(a => a.AppointmentInterval)
              .WithMany(ai => ai.Appointments)
              .HasForeignKey(a => a.AppointmentIntervalId)
              .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Patient>()
            .HasOne(p => p.BloodType)
            .WithMany(b => b.Patients)
            .HasForeignKey(p => p.BloodTypeId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
            .HasOne(g => g.Gender)
            .WithMany(u => u.Users)
            .HasForeignKey(p => p.GenderId)
            .OnDelete(DeleteBehavior.Restrict);


            // Seed data
            modelBuilder.Entity<BloodType>().HasData(
                new BloodType { Id = 1, Name = "A+" },
                new BloodType { Id = 2, Name = "A-" },
                new BloodType { Id = 3, Name = "B+" },
                new BloodType { Id = 4, Name = "B-" },
                new BloodType { Id = 5, Name = "AB+" },
                new BloodType { Id = 6, Name = "AB-" },
                new BloodType { Id = 7, Name = "O+" },
                new BloodType { Id = 8, Name = "O-" }
            );

            modelBuilder.Entity<AppointmentStatus>().HasData(
                new AppointmentStatus { Id = 1, Name = "Available" },
                new AppointmentStatus { Id = 2, Name = "Canceled" },
                new AppointmentStatus { Id = 3, Name = "Completed" },
                new AppointmentStatus { Id = 4, Name = "Created" }
            );

            modelBuilder.Entity<Branch>().HasData(
                new Branch { Id = 1, Name = "GeneralPractice" },
                new Branch { Id = 2, Name = "AnesthesiologyAndReanimation" },
                new Branch { Id = 3, Name = "Pediatrics" },
                new Branch { Id = 4, Name = "InternalMedicine" },
                new Branch { Id = 5, Name = "Dermatology" },
                new Branch { Id = 6, Name = "InfectiousDiseases" },
                new Branch { Id = 7, Name = "PhysicalMedicineAndRehabilitation" },
                new Branch { Id = 8, Name = "Gastroenterology" },
                new Branch { Id = 9, Name = "GeneralSurgery" },
                new Branch { Id = 10, Name = "Ophthalmology" },
                new Branch { Id = 11, Name = "ObstetricsAndGynecology" },
                new Branch { Id = 12, Name = "CardiovascularSurgery" },
                new Branch { Id = 13, Name = "Cardiology" },
                new Branch { Id = 14, Name = "Otorhinolaryngology" },
                new Branch { Id = 15, Name = "Neurology" },
                new Branch { Id = 16, Name = "Neurosurgery" },
                new Branch { Id = 17, Name = "OrthopedicsAndTraumatology" },
                new Branch { Id = 18, Name = "PlasticReconstructiveAndAestheticSurgery" },
                new Branch { Id = 19, Name = "Psychiatry" },
                new Branch { Id = 20, Name = "Radiology" },
                new Branch { Id = 21, Name = "Urology" }
            );

            modelBuilder.Entity<Gender>().HasData(
                new Gender { Id = 1, Name = "Bayan" },
                new Gender { Id = 2, Name = "Bay" }
            );

            modelBuilder.Entity<Domain.Entities.OperationClaim>().HasData(
                new Domain.Entities.OperationClaim { Id = 1, Name = "Admin" },
                new Domain.Entities.OperationClaim { Id = 2, Name = "Doctor" },
                new Domain.Entities.OperationClaim { Id = 3, Name = "Patient" }
            );

            modelBuilder.Entity<Title>().HasData(
                new Title { Id = 1, Name = "UzmDr" },
                new Title { Id = 2, Name = "Doc" },
                new Title { Id = 3, Name = "YrdDoc" },
                new Title { Id = 4, Name = "Prof" },
                new Title { Id = 5, Name = "OprDr" }
            );

            base.OnModelCreating(modelBuilder);
        }

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
    }
}
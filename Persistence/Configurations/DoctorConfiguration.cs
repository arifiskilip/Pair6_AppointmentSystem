using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                   .IsRequired();

            builder.Property(x => x.TitleId)
                   .IsRequired();

            builder.Property(x => x.BranchId)
                   .IsRequired();

            // Doctor ve Title arasındaki ilişki
            builder.HasOne(x => x.Title)
                   .WithMany(x => x.Doctors)
                   .HasForeignKey(x => x.TitleId);

            // Doctor ve Branch arasındaki ilişki
            builder.HasOne(x => x.Branch)
                   .WithMany(x => x.Doctors)
                   .HasForeignKey(x => x.BranchId);

            // Doctor ve User arasındaki ilişki
            builder.HasOne(x => x.User)
                   .WithOne(x => x.Doctor)
                   .HasForeignKey<Doctor>(x => x.UserId);

            // Doctor ve AppointmentInterval arasındaki ilişki
            builder.HasMany(x => x.AppointmentIntervals)
                   .WithOne(x => x.Doctor)
                   .HasForeignKey(x => x.DoctorId);

            // Doctor ve DoctorSchedule arasındaki ilişki
            builder.HasMany(x => x.DoctorSchedules)
                   .WithOne(x => x.Doctor)
                   .HasForeignKey(x => x.DoctorId);

            builder.ToTable("Doctors");
        }
    }
}

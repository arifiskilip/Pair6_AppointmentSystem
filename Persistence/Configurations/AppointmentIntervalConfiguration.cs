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
    public class AppointmentIntervalConfiguration : IEntityTypeConfiguration<AppointmentInterval>
    {
        public void Configure(EntityTypeBuilder<AppointmentInterval> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Day)
                   .IsRequired();

            builder.Property(x => x.Interval)
                   .IsRequired();

            // AppointmentInterval ve AppointmentStatus arasındaki ilişki
            builder.HasOne(x => x.AppointmentStatus)
                   .WithMany(x => x.AppointmentIntervals)
                   .HasForeignKey(x => x.AppointmentStatusId)
                   .OnDelete(DeleteBehavior.Restrict);

            // AppointmentInterval ve Doctor arasındaki bire çok ilişki
            builder.HasOne(x => x.Doctor)
                   .WithMany(x => x.AppointmentIntervals)
                   .HasForeignKey(x => x.DoctorId)
                    .OnDelete(DeleteBehavior.Restrict);

            // AppointmentInterval ve Appointment arasındaki bire bir ilişki
            builder.HasOne(x => x.Appointment)
                   .WithOne(x => x.AppointmentInterval)
                   .HasForeignKey<Appointment>(x => x.AppointmentIntervalId);

            builder.ToTable("AppointmentIntervals");
        
        }
    }
}

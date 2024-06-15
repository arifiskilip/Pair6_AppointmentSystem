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
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(x => x.Id);

            // Appointment ve Patient arasındaki ilişki
            builder.HasOne(x => x.Patient)
                   .WithMany(x => x.Appointments)
                   .HasForeignKey(x => x.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Appointment ve AppointmentInterval arasındaki bire çok ilişki
            //builder.HasOne(x => x.AppointmentInterval)
            //   .WithOne(x => x.Appointment)
            //   .HasForeignKey<Appointment>(x => x.AppointmentIntervalId);


            // Appointment ve Feedback arasındaki bire bir ilişki
            builder.HasOne(x => x.Feedback)
                   .WithOne(x => x.Appointment)
                   .HasForeignKey<Feedback>(x => x.AppointmentId);


            // Appointment ve Report arasındaki bire bir ilişki
            builder.HasOne(x => x.Report)
                   .WithOne(x => x.Appointment)
                   .HasForeignKey<Report>(x => x.AppointmentId);
                 

            builder.ToTable("Appointments");
        }
    }
}

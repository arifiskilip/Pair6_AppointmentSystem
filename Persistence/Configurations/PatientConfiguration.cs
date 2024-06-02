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
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BloodType).IsRequired();

            // Patient ve Appointment arasındaki ilişki
            builder.HasMany(x => x.Appointments)
                   .WithOne(x => x.Patient)
                   .HasForeignKey(x => x.PatientId);

            // Patient ve Feedback arasındaki ilişki
            builder.HasMany(x => x.Feedbacks)
                   .WithOne(x => x.Patient)
                   .HasForeignKey(x => x.PatientId);

            //builder.HasOne(x => x.User)
            //      .WithOne(x => x.Patient)
            //      .HasForeignKey<Patient>(x => x.UserId);

            builder.ToTable("Patients");
        }
    }
}

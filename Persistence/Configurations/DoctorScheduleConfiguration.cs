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
    public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
    {
        public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Day)
                   .IsRequired();

            builder.Property(x => x.StartTime)
                   .IsRequired();

            builder.Property(x => x.EndTime)
                   .IsRequired();

            builder.Property(x => x.PatientInterval)
                   .IsRequired();

            // DoctorSchedule ve Doctor arasındaki bire çok  ilişki
            builder.HasOne(x => x.Doctor)
                   .WithMany(x => x.DoctorSchedules)
                   .HasForeignKey(x => x.DoctorId);

            builder.ToTable("DoctorSchedules");
        }
    }
}

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
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                   .IsRequired();

            builder.Property(x => x.ReportFile)
                   .IsRequired();

            // Report ve Appointment arasındaki ilişki
            builder.HasOne(x => x.Appointment)
                   .WithOne(x => x.Report)
                   .HasForeignKey<Report>(x => x.AppointmentId);

            builder.ToTable("Reports");
        }
    }
}

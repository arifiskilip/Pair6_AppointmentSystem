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
    public class AppointmentStatusConfiguration : IEntityTypeConfiguration<AppointmentStatus>
    {
        public void Configure(EntityTypeBuilder<AppointmentStatus> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.HasMany(x => x.AppointmentIntervals)
                   .WithOne(x => x.AppointmentStatus)
                   .HasForeignKey(x => x.AppointmentStatusId);
                   

            builder.ToTable("AppointmentStatuses");
        }
    }
}

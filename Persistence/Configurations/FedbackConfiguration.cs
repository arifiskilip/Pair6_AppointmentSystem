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
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .IsRequired();

            // Feedback ve User arasındaki ilişki
            builder.HasOne(x => x.User)
                   .WithMany(x => x.Feedbacks)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Feedback ve Appointment arasındaki ilişki
            builder.HasOne(x => x.Appointment)
                   .WithOne(x => x.Feedback)
                   .HasForeignKey<Feedback>(x => x.AppointmentId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.ToTable("Feedbacks");
        }
    }
}

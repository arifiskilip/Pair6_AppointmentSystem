using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);

            // User ve Doctor arasındaki bire bir ilişki
            builder.HasOne(x => x.Doctor)
                   .WithOne(x => x.User)
                   .HasForeignKey<Doctor>(x => x.UserId);

            // User ve UserOperationClaim arasındaki bire çok ilişki
            builder.HasMany(x => x.UserOperationClaims)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId);

            // User ve Appointment arasındaki bire çok ilişki
            builder.HasMany(x => x.Appointments)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId);

            // User ve Feedback arasındaki bire çok ilişki
            builder.HasMany(x => x.Feedbacks)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId);

            builder.ToTable("Users");
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Persistence.Configurations
{
    public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                   .IsRequired();

            builder.Property(x => x.OperationClaimId)
                   .IsRequired();

            // UserOperationClaim ve User arasındaki ilişki
            builder.HasOne(x => x.User)
                   .WithMany(x => x.UserOperationClaims)
                   .HasForeignKey(x => x.UserId);

            // UserOperationClaim ve OperationClaim arasındaki ilişki
            builder.HasOne(x => x.OperationClaim)
                   .WithMany(x => x.UserOperationClaims)
                   .HasForeignKey(x => x.OperationClaimId);

            builder.ToTable("UserOperationClaims");
        }
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
    {
        public void Configure(EntityTypeBuilder<OperationClaim> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired();

            // OperationClaim ve UserOperationClaim arasındaki ilişki
            builder.HasMany(x => x.UserOperationClaims)
                   .WithOne(x => x.OperationClaim)
                   .HasForeignKey(x => x.OperationClaimId);

            builder.ToTable("OperationClaims");
        }
    }
}

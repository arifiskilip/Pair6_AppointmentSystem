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
    public class TitleConfiguration : IEntityTypeConfiguration<Title>
    {
        public void Configure(EntityTypeBuilder<Title> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired();

            // Title ve Doctor arasındaki bire çok ilişki
            builder.HasMany(x => x.Doctors)
                   .WithOne(x => x.Title)
                   .HasForeignKey(x => x.TitleId);

            builder.ToTable("Titles");
        }
    }
}

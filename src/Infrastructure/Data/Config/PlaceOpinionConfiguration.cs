using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{
    public class PlaceOpinionConfiguration : IEntityTypeConfiguration<PlaceOpinion>
    {
        public void Configure(EntityTypeBuilder<PlaceOpinion> builder)
        {
            builder.HasKey(b => b.ID);
            builder.Property(b => b.CreationDate).IsRequired().HasDefaultValue(new DateTime());
            builder.Property(b => b.LastModificationDate).IsRequired().HasDefaultValue(new DateTime());
            builder.Property(b => b.FirstName).IsRequired();
            builder.Property(b => b.FirstName).IsRequired();
            builder.Property(b => b.Rating).IsRequired();

        }
    }
}

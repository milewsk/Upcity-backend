using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Data.Config
{
    class PlaceDetailsConfiguration : IEntityTypeConfiguration<PlaceDetails>
    {
        public void Configure(EntityTypeBuilder<PlaceDetails> builder)
        {
            builder.HasKey(b => b.ID);
            builder.Property(b => b.IsDeleted).HasDefaultValue(0).IsRequired();
            builder.Property(b => b.CreationDate).IsRequired().HasDefaultValue(new DateTime());
            builder.Property(b => b.LastEditDate).IsRequired().HasDefaultValue(new DateTime());

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Data.Config
{
    public class PlaceOpeningHoursConfiguration : IEntityTypeConfiguration<PlaceOpeningHours>
    {
        public void Configure(EntityTypeBuilder<PlaceOpeningHours> builder)
        {
            builder.HasKey(b => b.ID);
            builder.Property(b => b.CreationDate).IsRequired().HasDefaultValue(new DateTime());
            builder.Property(b => b.LastModificationDate).IsRequired().HasDefaultValue(new DateTime());
            builder.Property(b => b.DayOfWeek).IsRequired();
            builder.Property(b => b.Opens).IsRequired();
            builder.Property(b => b.Closes).IsRequired();
        }
    }
}

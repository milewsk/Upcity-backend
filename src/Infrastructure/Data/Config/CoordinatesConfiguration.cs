using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class CoordinatesConfiguration : IEntityTypeConfiguration<Coordinates>
    {
        public void Configure(EntityTypeBuilder<Coordinates> builder)
        {
            builder.HasKey(b => b.ID);
            builder.Property(b => b.Latitude).IsRequired().HasColumnType("decimal(10,7)");
            builder.Property(b => b.Longitude).IsRequired().HasColumnType("decimal(10,7)");
        }
    }
}

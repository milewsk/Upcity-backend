using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{
    class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder.HasKey(b => b.ID);
            builder.Property(b => b.CreationDate).IsRequired().HasDefaultValue(new DateTime());
            builder.Property(b => b.LastModificationDate).IsRequired().HasDefaultValue(new DateTime());
            builder.Property(b => b.ProductID).IsRequired();
            builder.Property(b => b.TagID).IsRequired();
        }
    }
}

using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{
    public class PrivateMessageConfiguration : IEntityTypeConfiguration<PrivateMessage>
    {
        public void Configure(EntityTypeBuilder<PrivateMessage> builder)
        {
            builder.HasKey(b => b.ID);
            builder.Property(b => b.CreationDate).IsRequired().HasDefaultValue(new DateTime());
            builder.Property(b => b.LastModificationDate).IsRequired().HasDefaultValue(new DateTime());
            builder.Property(b => b.Title).IsRequired();
            builder.Property(b => b.Content).IsRequired();
        }
    }
}

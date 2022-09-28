using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{
    public class LoyalityProgramAccountConfiguration : IEntityTypeConfiguration<LoyalityProgramAccount>
    {
        public void Configure(EntityTypeBuilder<LoyalityProgramAccount> builder)
        {
            builder.HasKey(b => b.ID);
            builder.Property(b => b.CreationDate).IsRequired().HasDefaultValue(new DateTime());
            builder.Property(b => b.LastModificationDate).IsRequired().HasDefaultValue(new DateTime());

        }
    }
}

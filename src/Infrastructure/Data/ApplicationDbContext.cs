using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Infrastructure.Data

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UsersDetails { get; set; } 
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<LoyalityProgramAccount> LoyalityProgramAccounts { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceDetails> PlacesDetails { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PlaceTag> PlaceTags { get; set; }
        public DbSet<Coordinates> Coordinates { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<User>().HasOne<UserDetails>(u => u.UserDetails).WithOne(ud => ud.User).HasForeignKey<UserDetails>(ud => ud.UserID);
            builder.Entity<User>().HasOne<UserClaim>(u => u.UserClaim).WithOne(uc => uc.User).HasForeignKey<UserClaim>(uc => uc.UserID);
            builder.Entity<User>().HasOne<LoyalityProgramAccount>(u => u.LoyalityProgramAccount).WithOne(ud => ud.User).HasForeignKey<LoyalityProgramAccount>(ud => ud.UserID);

            builder.Entity<Place>().HasOne(p => p.Coordinates).WithOne(c => c.Place).HasForeignKey<Coordinates>(c => c.PlaceID);
            builder.Entity<Place>().HasOne(pd => pd.PlaceDetails).WithOne(c => c.Place).HasForeignKey<PlaceDetails>(c => c.PlaceID);
            builder.Entity<Place>().HasMany<Product>(p => p.Products).WithOne(pr => pr.Place).HasForeignKey(pr => pr.PlaceID);
            builder.Entity<Place>().HasMany<Table>(p => p.Tables).WithOne(t => t.Place).HasForeignKey(t => t.PlaceID).OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_Tables_Places_PlaceID");

            builder.Entity<PlaceTag>().HasKey(pt => pt.ID);
            builder.Entity<PlaceTag>().HasOne<Place>(pt => pt.Place).WithMany(p => p.PlaceTags).HasForeignKey(pt => pt.PlaceID);

            builder.Entity<ProductTag>().HasKey(pt => pt.ID);
            builder.Entity<ProductTag>().HasOne<Product>(pt => pt.Product).WithMany(p => p.ProductTags).HasForeignKey(pt => pt.ProductID);

            builder.Entity<Table>().HasMany<Reservation>(t => t.Reservations).WithOne(r => r.Table).HasForeignKey(r => r.TableID).OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_Reservation_Tables_TableID");
        }
    }
}

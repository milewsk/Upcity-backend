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
        public DbSet<UserLike> UserLikes { get; set; }

        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceDetails> PlacesDetails { get; set; }
        public DbSet<PlaceOpinion> PlaceOpinions { get; set; }
        public DbSet<PlaceMenu> PlaceMenus { get; set; }
        public DbSet<PlaceMenuCategory> PlaceMenuCategories { get; set; }
        public DbSet<PlaceOpeningHours> PlaceOpeningHours { get; set; }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<PlaceTag> PlaceTags { get; set; }
        public DbSet<Coordinates> Coordinates { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }


        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Promotion> Promotions { get; set; }


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
            builder.Entity<User>().HasMany<Reservation>(u => u.Reservations).WithOne(r => r.User).HasForeignKey(r => r.UserID);

            builder.Entity<Place>().HasOne(p => p.Coordinates).WithOne(c => c.Place).HasForeignKey<Coordinates>(c => c.PlaceID);
            builder.Entity<Place>().HasOne(p => p.PlaceDetails).WithOne(pd => pd.Place).HasForeignKey<PlaceDetails>(pd => pd.PlaceID);
            builder.Entity<Place>().HasOne(p => p.PlaceMenu).WithOne(pm => pm.Place).HasForeignKey<PlaceMenu>(pm => pm.PlaceID);
            builder.Entity<Place>().HasMany<PlaceOpinion>(p => p.PlaceOpinions).WithOne(po => po.Place).HasForeignKey(po => po.PlaceID);
            

            builder.Entity<PlaceMenu>().HasMany<PlaceMenuCategory>(pm => pm.PlaceMenuCategories).WithOne(pmc => pmc.PlaceMenu).HasForeignKey(pmc => pmc.PlaceMenuID);
            builder.Entity<PlaceMenuCategory>().HasMany<Product>(p => p.Products).WithOne(pr => pr.PlaceMenuCategory).HasForeignKey(pr => pr.PlaceMenuCategoryID);
            builder.Entity<Place>().HasMany<Reservation>(p => p.Reservations).WithOne(r => r.Place).HasForeignKey(r => r.PlaceID).OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_Reservation_Places_PlaceID");
            builder.Entity<Place>().HasMany<PlaceOpeningHours>(p => p.PlaceOpeningHours).WithOne(r => r.Place).HasForeignKey(r => r.PlaceID).OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_OpeningHours_Places_PlaceID");
            builder.Entity<Place>().HasMany<Promotion>(p => p.Promotions).WithOne(pr => pr.Place).HasForeignKey(pr => pr.PlaceID).OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_Promotion_Places_PlaceID");
            builder.Entity<Place>().HasMany<Message>(p => p.Messages).WithOne(m => m.Place).HasForeignKey(m => m.PlaceID).OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_Message_Places_PlaceID");

            builder.Entity<PlaceTag>().HasKey(pt => pt.ID);
            builder.Entity<PlaceTag>().HasOne<Place>(pt => pt.Place).WithMany(p => p.PlaceTags).HasForeignKey(pt => pt.PlaceID);

            builder.Entity<ProductTag>().HasKey(pt => pt.ID);
            builder.Entity<ProductTag>().HasOne<Product>(pt => pt.Product).WithMany(p => p.ProductTags).HasForeignKey(pt => pt.ProductID);

            builder.Entity<UserLike>().HasKey(pt => pt.ID);
            builder.Entity<UserLike>().HasOne<User>(ul => ul.User).WithMany(u => u.UserLikes).HasForeignKey(ul => ul.UserID);
        }
    }
}

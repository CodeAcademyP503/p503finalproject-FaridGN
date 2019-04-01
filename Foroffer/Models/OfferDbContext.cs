using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models
{
    public class OfferDbContext:IdentityDbContext<AppUser>
    {
        public OfferDbContext(DbContextOptions<OfferDbContext> DbContextOptions) : base(DbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CompanySubcategory>()
                .HasKey(a => new { a.CompanyId, a.SubcategoryId });

            modelBuilder.Entity<CompanySubcategory>()
                .HasOne(a => a.Company)
                .WithMany(b => b.CompanySubcategories)
                .HasForeignKey(a => a.CompanyId);

            modelBuilder.Entity<CompanySubcategory>()
                .HasOne(a => a.Subcategory)
                  .WithMany(c => c.CompanySubcategories)
                    .HasForeignKey(a => a.SubcategoryId);

           // modelBuilder.Entity<Post>().
            //    Property(p => p.CreatedDate)
             //   .HasColumnType("datetime2")
             //    .IsRequired();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<CompanySubcategory> CompanySubcategories { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<NestedCategory> NestedCategories { get; set; }
        public DbSet<Detailed> Detaileds { get; set; }
    }
}

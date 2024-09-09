using System;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Conge> Conges { get; set; }
        public DbSet<ChangementHoraire> ChangementHoraires { get; set; }
        public DbSet<BadgeManquant> BadgeManquants { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Componsation> Componsations { get; set; }
        public DbSet<TypeConge> TypeConges { get; set; }
        public DbSet<TypeShift> TypeShifts { get; set; }
        public DbSet<TimeAdjustment> TimeAdjustments { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Article> articles { get; set; }
        public DbSet<Comande> comandes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity-to-table mappings
            modelBuilder.Entity<Permission>()
                .ToTable("Permissions");

            modelBuilder.Entity<Componsation>()
                .ToTable("Componsations");
                // Configure entity-to-table mappings
        modelBuilder.Entity<Comande>()
            .HasOne(c => c.User)
            .WithMany() // Assumes a User has many Comandes, adjust if needed
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<Comande>()
            .HasOne(c => c.Department)
            .WithMany() // Assumes a Department has many Comandes, adjust if needed
            .HasForeignKey(c => c.DepartmentId);

        modelBuilder.Entity<Comande>()
            .HasMany(c => c.Articles)
            .WithMany() // Adjust if Articles have a relationship with Comande
            .UsingEntity(j => j.ToTable("ComandeArticles"));

            

            base.OnModelCreating(modelBuilder);
        }
    }
}

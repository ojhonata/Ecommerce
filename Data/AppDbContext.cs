using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public partial class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<Car>().HasKey(car => car.Id);
            modelBuilder.Entity<Category>().HasKey(category => category.Id);
            modelBuilder.Entity<Brand>().HasKey(brand => brand.Id);

            modelBuilder.Entity<Car>()
                .HasOne(car => car.Category)
                .WithMany(category => category.Cars)
                .HasForeignKey(car => car.CategoryId);

            modelBuilder.Entity<Car>()
                .HasOne(car => car.Brand)
                .WithMany(brand => brand.Cars)
                .HasForeignKey(car => car.BrandId);

            // --- CORREÇÃO FINAL E AGRESSIVA PARA O TiDB ---
            // Força todas as colunas GUID (Id) a usarem o collation compatível com o UTF8MB4.
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(Guid) || p.ClrType == typeof(Guid?)))
            {
                property.SetCollation("utf8mb4_general_ci");
            }
            // -------------------------------------------------------------

            base.OnModelCreating(modelBuilder);
        }
    }
}
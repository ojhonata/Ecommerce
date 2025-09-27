using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public partial class AppDbContext : DbContext // contexto do banco de dados
    {
        public DbSet<User> Usuarios { get; set; } // conjunto de usuários
        public DbSet<Car> Produtos { get; set; } // conjunto de produtos
        public DbSet<Category> Categorias { get; set; } // conjunto de categorias
        public DbSet<Brand> Marcas { get; set; } // conjunto de marcas

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(user => user.Id);

            modelBuilder.Entity<Car>()
                .HasKey(car => car.Id);

            modelBuilder.Entity<Category>()
                .HasKey(category => category.Id);

            modelBuilder.Entity<Brand>()
                .HasKey(brand => brand.Id);


            modelBuilder.Entity<Car>()
                .HasOne(car => car.Categoria)
                .WithMany(category => category.Produtos)
                .HasForeignKey(car => car.CategoriaId);

            modelBuilder.Entity<Car>()
                .HasOne(car => car.Marca)
                .WithMany(brand => brand.Produtos)
                .HasForeignKey(car => car.MarcaId);
                
            base.OnModelCreating(modelBuilder);
        }

    }
}
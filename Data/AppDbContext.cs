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
        public DbSet<Usuario> Usuarios { get; set; } // conjunto de usuários
        public DbSet<Produto> Produtos { get; set; } // conjunto de produtos
        public DbSet<Categoria> Categorias { get; set; } // conjunto de categorias
        public DbSet<Marca> Marcas { get; set; } // conjunto de marcas

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(usuario => usuario.Id);

            modelBuilder.Entity<Produto>()
                .HasKey(produto => produto.Id);

            modelBuilder.Entity<Categoria>()
                .HasKey(categoria => categoria.Id);

            modelBuilder.Entity<Marca>()
                .HasKey(marca => marca.Id);


            modelBuilder.Entity<Produto>()
                .HasOne(produto => produto.Categoria)
                .WithMany(categoria => categoria.Produtos)
                .HasForeignKey(produto => produto.CategoriaId);

            modelBuilder.Entity<Produto>()
                .HasOne(produto => produto.Marca)
                .WithMany(marca => marca.Produtos)
                .HasForeignKey(produto => produto.MarcaId);
                
            base.OnModelCreating(modelBuilder);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class AppDbContext : DbContext // contexto do banco de dados
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; } // conjunto de usuários
        public DbSet<Produto> Produtos { get; set; } // conjunto de produtos
        public DbSet<Categoria> Categorias { get; set; } // conjunto de categorias
        public DbSet<Marca> Marcas { get; set; } // conjunto de marcas
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Interface;
using Ecommerce.Models;

namespace Ecommerce.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<UsuarioModel> GetUsuarios()
        {
            return _context.ecommerce.ToList(); // retorna a lista de usuários do banco de dados
        }

    }
        
    
}
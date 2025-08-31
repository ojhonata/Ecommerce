using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Models;

namespace Ecommerce.Interface
{
    public interface IMarcaService
    {
        public List<string> GetMarcas();
        public Marca ObterMarcaPorId(int id);
        public Marca PostMarca(MarcaDTO marca);
        void UpdateMarca(Marca marca);
        void DeleteMarca(int id);
    }
}
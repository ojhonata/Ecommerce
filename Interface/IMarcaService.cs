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
        public List<Marca> GetMarcas();
        public Marca ObterMarcaPorId(Guid id);
        public Marca PostMarca(MarcaDTO marca);
        void UpdateMarca(Marca marca);
        void DeleteMarca(Guid id);
    }
}
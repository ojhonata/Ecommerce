using Ecommerce.DTOs;

namespace Ecommerce.Interface
{
    public interface IViaCepService
    {
        ViaCepDTO GetCep(string cep);
    }
}

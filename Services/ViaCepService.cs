using Ecommerce.DTOs;
using Ecommerce.Interface;
using System.Text.Json;

namespace Ecommerce.Services
{
    public class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;
        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://viacep.com.br/ws/");
        }
        public ViaCepDTO GetCep(string cep)
        {
            var response = _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/").Result;
            cep = cep.Replace("-", "").Trim();

            if (cep.Length != 8 || !cep.All(char.IsDigit))
                throw new ArgumentException("The provided ZIP code is invalid. It must contain 8 numeric digits.");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error when looking up the ZIP code.");

            var json = response.Content.ReadAsStringAsync().Result;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var result = JsonSerializer.Deserialize<ViaCepDTO>(json, options);

            if (result == null || result.Cep == null)
                throw new Exception("ZIP code not found.");

            return result;
        }

    }
}

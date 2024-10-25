using System.Text.Json;
using TestBlazorApp.Modelos;

namespace TestBlazorApp.Servicios
{
    public class CategoriaService: ICategoriaService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public CategoriaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            
        }


        public async Task<List<Categoria>?> GetCategorias()
        {
            var response = await _httpClient.GetAsync("v1/categories");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            return JsonSerializer.Deserialize<List<Categoria>>(content, _options);
        }



    }
}


public interface ICategoriaService
{
    Task<List<Categoria>?> GetCategorias();
}
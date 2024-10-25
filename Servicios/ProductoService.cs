
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using TestBlazorApp.Modelos;

namespace TestBlazorApp.Servicios

{
    public class ProductoService: IProductoService
    {
        public readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;
        public ProductoService(HttpClient client)
        {
            _client = client;
            _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        //public async Task<List<Producto>?> GetProductos()
        //{
        //    var response = await _client.GetAsync("v1/products");
        //    return await JsonSerializer.DeserializeAsync<List<Producto>>(await response.Content.ReadAsStreamAsync());
        //}

        public async Task<List<Producto>?> Get()
        {
            var response = await _client.GetAsync("v1/products");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            return JsonSerializer.Deserialize<List<Producto>>(content, _serializerOptions);
        }

        public async Task AddProducto(Producto producto)
        {
            var response = await _client.PostAsync("v1/products",JsonContent.Create(producto));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }

        public async Task Delete(int productoId)
        {
            var response = await _client.DeleteAsync($"v1/products/{productoId}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }


    }
}


public interface IProductoService
{
    Task<List<Producto>?> Get();
    Task AddProducto(Producto producto);
    Task Delete(int productoId);

}

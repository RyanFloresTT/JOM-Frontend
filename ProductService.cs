using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

public class ProductService : IProductService {
    private readonly HttpClient _http;

    public List<Product> Products { get; set; }

    public ProductService(HttpClient http)
    {
        _http = http;
        Products = new List<Product>();
    }

    public async Task<List<Product>> GetProductsAsync() {
        try {
            Products = await _http.GetFromJsonAsync<List<Product>>("api/products") ?? new List<Product>();
        }
        catch (Exception ex) {
            Console.WriteLine(ex);
        }
        return Products;
    }

}

public class Product {
    public int id { get; set; }
    public string name { get; set; }
    public string image { get; set; }
    public string priceID { get; set; }
    public float price { get; set; }
}
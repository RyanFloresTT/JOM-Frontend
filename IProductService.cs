public interface IProductService {
    public List<Product> Products { get; set; }
    public Task<List<Product>> GetProductsAsync();
}

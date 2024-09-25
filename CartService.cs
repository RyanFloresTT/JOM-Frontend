using Blazored.LocalStorage;

public class CartService {
    private readonly ILocalStorageService _localStorage;
    public event Action OnChange;

    public CartService(ILocalStorageService localStorage) {
        _localStorage = localStorage;
    }

    public async Task<List<Product>> GetCartItemsAsync() {
        return await _localStorage.GetItemAsync<List<Product>>("cart") ?? new List<Product>();
    }

    public async Task AddToCartAsync(Product product) {
        var cart = await GetCartItemsAsync();

        var existingProduct = cart.FirstOrDefault(p => p.id == product.id);
        if (existingProduct != null) {
            existingProduct.Quantity++;
        } else {
            cart.Add(product);
        }

        await _localStorage.SetItemAsync("cart", cart);
        NotifyStateChanged();
    }


    public async Task<int> GetCartCountAsync() {
        var cart = await GetCartItemsAsync();
        return cart.Count;
    }

    public async Task RemoveCartItemAsync(Product product) {
        var cart = await GetCartItemsAsync();
        var item = cart.FirstOrDefault(i => i.priceID == product.priceID);

        if (item != null) {
            cart.Remove(item);
            await _localStorage.SetItemAsync("cart", cart);
            NotifyStateChanged();
        }
    }

    public async Task UpdateCartItemAsync(Product product) {
        var cart = await GetCartItemsAsync();
        var item = cart.FirstOrDefault(i => i.priceID == product.priceID);
        if (item != null) {
            item.Quantity = product.Quantity;
            await _localStorage.SetItemAsync("cart", cart);
            NotifyStateChanged();
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke();

}

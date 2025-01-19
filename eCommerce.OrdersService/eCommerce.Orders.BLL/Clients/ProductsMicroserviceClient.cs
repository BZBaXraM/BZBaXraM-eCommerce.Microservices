namespace eCommerce.Orders.BLL.Clients;

public class ProductsMicroserviceClient(HttpClient client)
{
    public async Task<ProductDto?> GetProductByIdAsync(Guid id)
    {
        try
        {
            var response = await client.GetAsync($"api/products/search/product-id/{id}");

            if (!response.IsSuccessStatusCode) return null;

            var product = await response.Content.ReadFromJsonAsync<ProductDto>();

            return product;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request error: {ex.Message}");
            return null;
        }
    }
}
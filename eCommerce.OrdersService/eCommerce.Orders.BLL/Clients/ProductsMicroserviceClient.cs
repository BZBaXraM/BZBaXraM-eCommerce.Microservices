using System.Net;

namespace eCommerce.Orders.BLL.Clients;

public class ProductsMicroserviceClient(HttpClient client)
{
    public async Task<ProductDto?> GetProductByIdAsync(Guid productId)
    {
        var response = await client.GetAsync($"api/products/search/product-id/{productId}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response JSON: {jsonResponse}");


        var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        var product = products?.FirstOrDefault();

        if (product == null)
        {
            throw new ArgumentException($"Product with ID {productId} not found.");
        }

        return product;
    }
}
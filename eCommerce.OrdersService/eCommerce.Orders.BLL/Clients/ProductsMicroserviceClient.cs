using System.Net;

namespace eCommerce.Orders.BLL.Clients;

public class ProductsMicroserviceClient(HttpClient client)
{
    public async Task<ProductDto?> GetProductByIdAsync(Guid id)
    {
        var response = await client.GetAsync($"api/products/search/product-id/{id}");

        if (!response.IsSuccessStatusCode)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.NotFound => null,
                HttpStatusCode.BadRequest => throw new HttpRequestException("Bad request", null,
                    HttpStatusCode.BadRequest),
                _ => throw new HttpRequestException($"Http request failed with status code {response.StatusCode}")
            };
        }


        var product = await response.Content.ReadFromJsonAsync<ProductDto>();

        if (product == null)
        {
            throw new ArgumentException("Invalid Product ID");
        }

        return product;
    }
}
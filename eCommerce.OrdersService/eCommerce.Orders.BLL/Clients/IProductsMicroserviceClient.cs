using Refit;

namespace eCommerce.Orders.BLL.Clients;

public interface IProductsMicroserviceClient
{
    [Get("/api/Products/search/product-id/{productId}")]
    [Headers("Content-Type: application/json")]
    Task<List<ProductDto>> GetProductByIdAsync(Guid productId);
}

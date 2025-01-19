namespace eCommerce.Products.BLL.Services;

public interface IProductsService
{
    Task<IReadOnlyList<ProductResponse?>> GetProductsAsync();
    Task<IReadOnlyList<ProductResponse?>> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression);
    Task<ProductResponse?> AddProductAsync(ProductAddRequest productAddRequest);
    Task<ProductResponse?> UpdateProductAsync(ProductUpdateRequest productUpdateRequest);
    Task<bool> DeleteProductAsync(Guid productId);
}
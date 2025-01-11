using System.Linq.Expressions;
using eCommerce.Products.DAL.Entities;

namespace eCommerce.Products.DAL.Repositories;

public interface IProductsRepository
{
    Task<IReadOnlyList<Product?>> GetProductsAsync();
    Task<IReadOnlyList<Product?>> GetProductByConditionAsync(Expression<Func<Product, bool>> conditionExpression);
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<Product?> AddProductAsync(Product product);
    Task<Product?> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(Guid id);
}
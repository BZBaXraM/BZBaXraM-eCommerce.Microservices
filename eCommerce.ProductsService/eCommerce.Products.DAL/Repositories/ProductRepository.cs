namespace eCommerce.Products.DAL.Repositories;

public class ProductRepository(ProductContext context) : IProductsRepository
{
    public async Task<IReadOnlyList<Product?>> GetProductsAsync()
    {
        return await context.Products.ToListAsync();
    }

    public async Task<IReadOnlyList<Product?>> GetProductByConditionAsync(
        Expression<Func<Product, bool>> conditionExpression)
    {
        return await context.Products.Where(conditionExpression).ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task<Product?> AddProductAsync(Product product)
    {
        var entity = await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<Product?> UpdateProductAsync(Product product)
    {
        var entity = context.Products.Update(product);
        await context.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        var product = await context.Products.FindAsync(id);
        if (product is not null)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }

        return product is not null;
    }
}
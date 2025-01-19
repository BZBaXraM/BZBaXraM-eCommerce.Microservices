namespace eCommerce.Products.BLL.Services;

public class ProductsService(
    IProductsRepository repository,
    IValidator<ProductAddRequest> productAddRequestValidator,
    IValidator<ProductUpdateRequest> productUpdateRequestValidator) : IProductsService
{
    public async Task<IReadOnlyList<ProductResponse?>> GetProductsAsync()
    {
        var products = await repository.GetProductsAsync();
        return products.Select(p => new ProductResponse
        {
            ProductId = p!.ProductId,
            ProductName = p.Name,
            Category = p.Category,
            UnitPrice = p.UnitPrice,
            QuantityInStock = p.QuantityInStock,
        }).ToList();
    }

    public async Task<IReadOnlyList<ProductResponse?>> GetProductByCondition(
        Expression<Func<Product, bool>> conditionExpression)
    {
        var products = await repository.GetProductByConditionAsync(conditionExpression);
        return products.Select(p => new ProductResponse
        {
            ProductId = p!.ProductId,
            ProductName = p.Name,
            Category = p.Category,
            UnitPrice = p.UnitPrice,
            QuantityInStock = p.QuantityInStock,
        }).ToList();
    }

    public async Task<ProductResponse?> AddProductAsync(ProductAddRequest productAddRequest)
    {
        if (!(await productAddRequestValidator.ValidateAsync(productAddRequest)).IsValid)
        {
            throw new ValidationException("Invalid ProductAddRequest");
        }

        var product = new Product
        {
            Name = productAddRequest.ProductName,
            Category = productAddRequest.Category,
            UnitPrice = productAddRequest.UnitPrice,
            QuantityInStock = productAddRequest.QuantityInStock
        };

        var addedProduct = await repository.AddProductAsync(product);

        return new ProductResponse
        {
            ProductId = addedProduct!.ProductId,
            ProductName = addedProduct.Name,
            Category = addedProduct.Category,
            UnitPrice = addedProduct.UnitPrice,
            QuantityInStock = addedProduct.QuantityInStock,
        };
    }

    public async Task<ProductResponse?> UpdateProductAsync(ProductUpdateRequest productUpdateRequest)
    {
        if (!(await productUpdateRequestValidator.ValidateAsync(productUpdateRequest)).IsValid)
        {
            throw new ValidationException("Invalid ProductUpdateRequest");
        }

        var product = new Product
        {
            ProductId = productUpdateRequest.ProductId,
            Name = productUpdateRequest.ProductName,
            Category = productUpdateRequest.Category,
            UnitPrice = productUpdateRequest.UnitPrice,
            QuantityInStock = productUpdateRequest.QuantityInStock
        };

        var updatedProduct = await repository.UpdateProductAsync(product);

        return new ProductResponse
        {
            ProductId = updatedProduct!.ProductId,
            ProductName = updatedProduct.Name,
            Category = updatedProduct.Category,
            UnitPrice = updatedProduct.UnitPrice,
            QuantityInStock = updatedProduct.QuantityInStock,
        };
    }

    public async Task<bool> DeleteProductAsync(Guid productId)
    {
        var isDeleted = await repository.DeleteProductAsync(productId);
        
        if (!isDeleted)
        {
            throw new ArgumentException("Invalid Product ID");
        }


        return isDeleted;
    }
}
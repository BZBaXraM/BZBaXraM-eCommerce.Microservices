namespace eCommerce.Orders.BLL.DTOs;

public class ProductDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }
    public decimal? UnitPrice { get; set; }
    public int? QuantityInStock { get; set; }

    public ProductDto(Guid productId, string productName, string category, decimal? unitPrice, int? quantityInStock)
    {
        ProductId = productId;
        ProductName = productName;
        Category = category;
        UnitPrice = unitPrice;
        QuantityInStock = quantityInStock;
    }
}
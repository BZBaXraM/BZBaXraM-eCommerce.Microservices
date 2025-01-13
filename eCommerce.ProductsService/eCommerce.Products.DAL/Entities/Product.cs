namespace eCommerce.Products.DAL.Entities;

public class Product
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal? UnitPrice { get; set; }
    public int? QuantityInStock { get; set; }
}
namespace eCommerce.Orders.DAL.Entities;

public class OrderItem
{
    [BsonRepresentation(BsonType.String)] public Guid _id { get; set; }
    [BsonRepresentation(BsonType.String)] public Guid ProductId { get; set; }

    [BsonRepresentation(BsonType.Double)] public decimal UnitPrice { get; set; }

    [BsonRepresentation(BsonType.Int32)] public int Quantity { get; set; }

    [BsonRepresentation(BsonType.Double)] public decimal TotalPrice { get; set; }
}
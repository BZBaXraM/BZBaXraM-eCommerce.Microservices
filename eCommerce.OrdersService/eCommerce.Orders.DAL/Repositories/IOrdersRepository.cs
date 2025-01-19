namespace eCommerce.Orders.DAL.Repositories;

public interface IOrdersRepository
{
    Task<IReadOnlyList<Order>> GetOrders();
    Task<IReadOnlyList<Order?>> GetOrdersByCondition(FilterDefinition<Order> filter);
    Task<Order?> GetOrderByCondition(FilterDefinition<Order> filter);
    Task<Order?> AddOrder(Order order);
    Task<Order?> UpdateOrder(Order order);
    Task<bool> DeleteOrder(Guid orderId);
}
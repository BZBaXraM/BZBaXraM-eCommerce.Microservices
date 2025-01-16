using eCommerce.Orders.DAL.DTOs;
using eCommerce.Orders.DAL.Entities;
using MongoDB.Driver;

namespace eCommerce.Orders.BLL.Services;

public interface IOrdersService
{
    Task<IReadOnlyList<OrderResponse?>> GetOrdersAsync();
    Task<IReadOnlyList<OrderResponse?>> GetOrdersByConditionAsync(FilterDefinition<Order> filter);
    Task<OrderResponse?> GetOrderByConditionAsync(FilterDefinition<Order> filter);
    Task<OrderResponse?> AddOrderAsync(OrderAddRequest? request);
    Task<OrderResponse?> UpdateOrderAsync(OrderUpdateRequest request);
    Task<bool> DeleteOrderAsync(Guid id);
}
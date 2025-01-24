using Refit;

namespace eCommerce.Orders.BLL.Clients;

public interface IUsersMicroserviceClient
{
    [Get("/api/Users/{userId}")]
    Task<UserDto?> GetUserByIdAsync(Guid userId);
}
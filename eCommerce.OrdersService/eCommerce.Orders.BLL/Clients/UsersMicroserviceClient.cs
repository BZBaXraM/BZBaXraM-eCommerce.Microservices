using System.Net.Http.Json;
using eCommerce.Orders.BLL.DTOs;

namespace eCommerce.Orders.BLL.Clients;

public class UsersMicroserviceClient(HttpClient client)
{
    public async Task<UserDto?> GetUserByIdAsync(Guid userId)
    {
        var response = await client.GetAsync($"api/users/{userId}");

        if (!response.IsSuccessStatusCode) return null;

        var user = await response.Content.ReadFromJsonAsync<UserDto>();

        return user;
    }
}
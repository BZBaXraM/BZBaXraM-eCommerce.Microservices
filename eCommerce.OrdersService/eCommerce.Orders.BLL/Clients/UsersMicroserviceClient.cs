namespace eCommerce.Orders.BLL.Clients;

public class UsersMicroserviceClient(HttpClient client)
{
    public async Task<UserDto?> GetUserByIdAsync(Guid userId)
    {
        try
        {
            var response = await client.GetAsync($"api/users/{userId}");

            if (!response.IsSuccessStatusCode) return null;

            var user = await response.Content.ReadFromJsonAsync<UserDto>();

            return user;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request error: {ex.Message}");
            return null;
        }
    }
}
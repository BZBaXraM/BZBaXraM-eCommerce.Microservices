namespace eCommerce.Users.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUsersService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUsersAsync()
    {
        await Task.Delay(100);

        throw new NotImplementedException();

        var users = await service.GetUsersAsync();

        return Ok(users);
    }

    [HttpGet("{userId:guid}")]
    public async Task<UserDto?> GetUserByUserIdAsync(Guid userId)
    {
        return await service.GetUserByUserIdAsync(userId);
    }

    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> DeleteUserAsync(Guid userId)
    {
        if (userId == Guid.Empty) return BadRequest("Invalid user id");

        var isDeleted = await service.DeleteUserAsync(userId);

        if (!isDeleted) return BadRequest("User not found");

        return Ok();
    }
}
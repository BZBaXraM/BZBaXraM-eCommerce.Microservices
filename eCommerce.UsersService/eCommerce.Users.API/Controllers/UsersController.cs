namespace eCommerce.Users.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUsersService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUsersAsync()
    {
        var users = await service.GetUsersAsync();

        return Ok(users);
    }

    [HttpGet("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<UserDto?> GetUserByUserIdAsync(Guid userId)
    {
        return await service.GetUserByUserIdAsync(userId);
    }

    [HttpDelete("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUserAsync(Guid userId)
    {
        if (userId == Guid.Empty) return BadRequest("Invalid user id");

        var isDeleted = await service.DeleteUserAsync(userId);

        if (!isDeleted) return BadRequest("User not found");

        return Ok();
    }
}
namespace eCommerce.Users.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUsersService service) : ControllerBase
{
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserByUserIdAsync(Guid userId)
    {
        if (userId == Guid.Empty) return BadRequest("Invalid user id");

        var user = await service.GetUserByUserIdAsync(userId);

        if (user == null) return NotFound();

        return Ok(user);
    }
}
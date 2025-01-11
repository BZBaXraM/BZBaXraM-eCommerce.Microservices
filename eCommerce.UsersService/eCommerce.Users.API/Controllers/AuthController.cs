namespace eCommerce.Users.API.Controllers;

[Route("api/[controller]")] // api/auth
[ApiController]
public class AuthController(IUsersService usersService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (request is null)
        {
            return BadRequest("Invalid registration data");
        }

        var response = await usersService.RegisterAsync(request);

        if (response is null || response.Success is false)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult?> Login([FromBody] LoginRequest request)
    {
        if (request is null)
        {
            return BadRequest("Invalid login data");
        }

        var response = await usersService.LoginAsync(request);

        if (response is null || response.Success is false)
        {
            return Unauthorized(response);
        }

        return Ok(response);
    }
}
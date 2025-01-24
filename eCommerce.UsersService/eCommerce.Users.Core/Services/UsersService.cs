namespace eCommerce.Users.Core.Services;

public class UsersService(IUsersRepository usersRepository, IMapper mapper) : IUsersService
{
    public async Task<AuthResponse?> RegisterAsync(RegisterRequest request)
    {
        var user = new AppUser
        {
            Email = request.Email,
            Password = request.Password,
            PersonName = request.PersonName,
            Gender = request.Gender.ToString()
        };

        var registeredUser = await usersRepository.AddUserAsync(user);

        if (registeredUser is null)
        {
            return null;
        }

        return mapper.Map<AuthResponse>(registeredUser)
            with
            {
                Success = true, Token = "token"
            };
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var user = await usersRepository.GetUserByEmailAndPasswordAsync(request.Email, request.Password);

        return user is null
            ? null
            : mapper.Map<AuthResponse>(user)
                with
                {
                    Success = true, Token = "token"
                };
    }

    public async Task<UserDto> GetUserByUserIdAsync(Guid userId)
    {
        var user = await usersRepository.GetUserByUserIdAsync(userId);

        return mapper.Map<UserDto>(user);
    }

    public async Task<bool> DeleteUserAsync(Guid userId)
    {
        return await usersRepository.DeleteUserAsync(userId);
    }

    public async Task<IReadOnlyList<AppUser>> GetUsersAsync()
    {
        return await usersRepository.GetUsersAsync();
    }
}
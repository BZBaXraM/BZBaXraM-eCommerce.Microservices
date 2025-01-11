using eCommerce.Users.Core.DTOs;
using eCommerce.Users.Core.Entities;
using eCommerce.Users.Core.Repository;

namespace eCommerce.Users.Core.Services;

internal class UsersService(IUsersRepository usersRepository, IMapper mapper) : IUsersService
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
}
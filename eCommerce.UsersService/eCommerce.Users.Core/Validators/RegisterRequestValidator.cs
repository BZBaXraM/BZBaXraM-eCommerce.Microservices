using eCommerce.Users.Core.DTOs;
using FluentValidation;

namespace eCommerce.Users.Core.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email format");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MaximumLength(15)
            .MinimumLength(6);

        RuleFor(x => x.PersonName)
            .NotEmpty()
            .WithMessage("Person name is required");

        RuleFor(x => x.Gender)
            .IsInEnum()
            .WithMessage("Invalid enum value");
    }
}
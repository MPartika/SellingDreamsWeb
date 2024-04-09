using FluentValidation;

namespace SellingDreamsCommandHandler.Authenticate;

public class CreateLoginValidator : AbstractValidator<CreateLoginCommand>
{
    public CreateLoginValidator()
    {
        RuleFor(login => login.UserName)
            .NotNull().WithMessage("User name cannot be empty")
            .NotEmpty().WithMessage("User name cannot be empty");
        RuleFor(login => login.Password)
            .NotEmpty().WithMessage("Your password cannot be empty")
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.");
    }
}

using FluentValidation;

namespace SellingDreamsCommandHandler.Authenticate;

public class PatchLoginValidator : AbstractValidator<PatchLoginCommand>
{
    public PatchLoginValidator()
    {
        RuleFor(login => login.Password)
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .When(login => !string.IsNullOrEmpty(login.Password));
    }
}

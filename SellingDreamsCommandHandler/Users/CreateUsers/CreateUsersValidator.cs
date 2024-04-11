using System.Text.RegularExpressions;
using FluentValidation;

namespace SellingDreamsCommandHandler.Users;

public class CreateUsersValidator : AbstractValidator<CreateUsersCommand>
{
    public CreateUsersValidator()
    {
        RuleFor(rule => rule.EmailAddress)
            .NotEmpty().WithMessage("You must provide email")
            .EmailAddress().WithMessage("You must provide valid email address");
        RuleFor(rule => rule.Name)
            .NotEmpty().WithMessage("You must provide user name")
            .Must(name => name.Contains(" "))
            .WithMessage("You must provide name and surname");
        RuleFor(rule => rule.PhoneNumber)
            .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
            .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
            .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("PhoneNumber not valid")
            .When(rule => !string.IsNullOrEmpty(rule.PhoneNumber));
    }
}

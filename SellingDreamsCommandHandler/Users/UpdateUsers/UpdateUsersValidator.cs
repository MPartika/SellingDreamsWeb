using System.Text.RegularExpressions;
using FluentValidation;

namespace SellingDreamsCommandHandler.Users;

public class UpdateUsersValidator : AbstractValidator<UpdateUsersCommand>
{
    public UpdateUsersValidator()
    {
        RuleFor(user => user.UserId)
            .NotEmpty().WithMessage("UserId is required");
        RuleFor(rule => rule.PhoneNumber)
            .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
            .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
            .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("PhoneNumber not valid")
            .When(rule => !string.IsNullOrEmpty(rule.PhoneNumber));
    }
}

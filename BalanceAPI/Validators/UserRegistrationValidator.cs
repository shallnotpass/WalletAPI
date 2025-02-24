using Application.Models;
using FluentValidation;

namespace BalanceAPI.Validators
{
    public class UserRegistrationValidator : AbstractValidator<UserRegistrationRequest>
    {
        public UserRegistrationValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
        }
    }
}

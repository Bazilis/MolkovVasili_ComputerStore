using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace ComputerStore.BLL.Validators
{
    public class UserValidator : AbstractValidator<IdentityUser>
    {
        public UserValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("User", () =>
            {
                RuleFor(x => x.UserName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(30);

                RuleFor(x => x.Email)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(30)
                    .EmailAddress();
            });
        }
    }
}

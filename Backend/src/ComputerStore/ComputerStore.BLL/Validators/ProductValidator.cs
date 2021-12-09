using ComputerStore.BLL.Models;
using FluentValidation;

namespace ComputerStore.BLL.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("Product", () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(30);

                RuleFor(x => x.QuantityInStorage)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("The Quantity in storage must be positive number");
            });
        }
    }
}

using ComputerStore.BLL.Models.CategoryCharacteristics.Int;
using FluentValidation;

namespace ComputerStore.BLL.Validators.CategoryCharacteristics.Int
{
    public class ProductCharacteristicIntValidator : AbstractValidator<ProductCharacteristicIntDto>
    {
        public ProductCharacteristicIntValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("CategoryCharacteristicInt", () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(30);

                RuleFor(x => x.Dimension)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(30);

                RuleFor(x => x.CharacteristicValueInt)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("The ValueInt must be greater than or equal to 0");
            });
        }
    }
}

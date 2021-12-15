using ComputerStore.BLL.Models.CategoryCharacteristics.Double;
using FluentValidation;

namespace ComputerStore.BLL.Validators.CategoryCharacteristics.Double
{
    public class ProductCharacteristicDoubleValidator : AbstractValidator<ProductCharacteristicDoubleDto>
    {
        public ProductCharacteristicDoubleValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("CategoryCharacteristicDouble", () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(30);

                RuleFor(x => x.Dimension)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(30);

                RuleFor(x => x.CharacteristicValueDouble)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("The ValueDouble must be greater than or equal to 0");
            });
        }
    }
}

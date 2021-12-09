using ComputerStore.BLL.Models.CategoryCharacteristics.Double;
using FluentValidation;

namespace ComputerStore.BLL.Validators.CategoryCharacteristics.Double
{
    public class CategoryCharacteristicDoubleValidator : AbstractValidator<ProductCharacteristicDoubleDto>
    {
        public CategoryCharacteristicDoubleValidator()
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
            });
        }
    }
}

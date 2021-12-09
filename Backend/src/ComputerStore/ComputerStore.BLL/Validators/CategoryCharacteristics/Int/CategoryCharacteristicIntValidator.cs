using ComputerStore.BLL.Models.CategoryCharacteristics.Int;
using FluentValidation;

namespace ComputerStore.BLL.Validators.CategoryCharacteristics.Int
{
    public class CategoryCharacteristicIntValidator : AbstractValidator<ProductCharacteristicIntDto>
    {
        public CategoryCharacteristicIntValidator()
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
            });
        }
    }
}

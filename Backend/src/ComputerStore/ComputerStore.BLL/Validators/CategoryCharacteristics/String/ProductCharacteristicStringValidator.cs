using ComputerStore.BLL.Models.CategoryCharacteristics.String;
using FluentValidation;

namespace ComputerStore.BLL.Validators.CategoryCharacteristics.String
{
    public class ProductCharacteristicStringValidator : AbstractValidator<ProductCharacteristicStringDto>
    {
        public ProductCharacteristicStringValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("CategoryCharacteristicString", () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(30);

                RuleFor(x => x.Dimension)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(30);

                RuleFor(x => x.CharacteristicValueString)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(30);
            });
        }
    }
}

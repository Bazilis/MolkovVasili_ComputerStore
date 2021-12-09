using ComputerStore.BLL.Models.CategoryCharacteristics.String;
using FluentValidation;

namespace ComputerStore.BLL.Validators.CategoryCharacteristics.String
{
    public class CategoryCharacteristicStringValidator : AbstractValidator<ProductCharacteristicStringDto>
    {
        public CategoryCharacteristicStringValidator()
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
            });
        }
    }
}

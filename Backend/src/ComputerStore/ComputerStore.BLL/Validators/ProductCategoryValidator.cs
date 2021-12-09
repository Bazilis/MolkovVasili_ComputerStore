using ComputerStore.BLL.Models;
using FluentValidation;

namespace ComputerStore.BLL.Validators
{
    public class ProductCategoryValidator : AbstractValidator<ProductCategoryDto>
    {
        public ProductCategoryValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("ProductCategory", () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(30);
            });
        }
    }
}

using ComputerStore.BLL.Models;
using FluentValidation;

namespace ComputerStore.BLL.Validators
{
    public class OrderValidator : AbstractValidator<OrderDto>
    {
        public OrderValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("Order", () =>
            {
                RuleFor(x => x.Quantity)
                    .GreaterThan(0)
                    .WithMessage("The Quantity of Products must be greater than 0");
            });
        }
    }
}

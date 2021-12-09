using ComputerStore.BLL.Models.CategoryCharacteristics.Int;
using FluentValidation;

namespace ComputerStore.BLL.Validators.CategoryCharacteristics.Int
{
    public class CharacteristicValueIntValidator : AbstractValidator<CharacteristicValueIntDto>
    {
        public CharacteristicValueIntValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("CharacteristicValue", () =>
            {
                RuleFor(x => x.ValueInt)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("The ValueInt must be greater than or equal to 0");
            });
        }
    }
}

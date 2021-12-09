using ComputerStore.BLL.Models.CategoryCharacteristics.Double;
using FluentValidation;

namespace ComputerStore.BLL.Validators.CategoryCharacteristics.Double
{
    public class CharacteristicValueDoubleValidator : AbstractValidator<CharacteristicValueDoubleDto>
    {
        public CharacteristicValueDoubleValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("CharacteristicValueDouble", () =>
            {
                RuleFor(x => x.ValueDouble)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("The ValueDouble must be greater than or equal to 0");
            });
        }
    }
}

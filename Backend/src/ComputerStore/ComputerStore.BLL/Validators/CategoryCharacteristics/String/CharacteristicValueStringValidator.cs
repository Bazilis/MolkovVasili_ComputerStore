using ComputerStore.BLL.Models.CategoryCharacteristics.String;
using FluentValidation;

namespace ComputerStore.BLL.Validators.CategoryCharacteristics.String
{
    public class CharacteristicValueStringValidator : AbstractValidator<CharacteristicValueStringDto>
    {
        public CharacteristicValueStringValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("CharacteristicValue", () =>
            {
                RuleFor(x => x.ValueString)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(30);
            });
        }
    }
}

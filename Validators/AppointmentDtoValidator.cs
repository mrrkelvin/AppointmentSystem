using AppointmentSystem.Dtos;
using FluentValidation;

namespace AppointmentSystem.Validators
{
    public class AppointmentDtoValidator : AbstractValidator<AppointmentDto>
    {
        public AppointmentDtoValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.SlotId)
                .GreaterThan(0);
        }
    }
}
using AppointmentSystem.Dtos;
using FluentValidation;

namespace AppointmentSystem.Validators
{
    public class SettingsDtoValidator : AbstractValidator<SettingsDto>
    {
        public SettingsDtoValidator()
        {
            RuleFor(x => x.SlotDurationMinutes)
                .NotEmpty()
                .GreaterThanOrEqualTo(5)
                    .WithMessage("{PropertyName} must be at least 5 minutes.");

            RuleFor(x => x.MaxBookingsPerSlot)
                .NotEmpty()
                .InclusiveBetween(1, 5);

            RuleFor(x => x.WorkingHoursStart)
                .NotEmpty()
                .Matches(@"^\d{2}:\d{2}$")
                    .WithMessage("{PropertyName} must be in HH:mm format.");

            RuleFor(x => x.WorkingHoursEnd)
                .NotEmpty()
                .Matches(@"^\d{2}:\d{2}$")
                    .WithMessage("{PropertyName} must be in HH:mm format.");
        }
    }
}
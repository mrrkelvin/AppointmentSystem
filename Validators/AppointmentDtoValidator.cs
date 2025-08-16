using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                    .WithMessage("Customer name is required.")
                .MaximumLength(100)
                    .WithMessage("Customer name cannot exceed 100 characters.");

            RuleFor(x => x.SlotId)
                .GreaterThan(0)
                    .WithMessage("SlotId must be greater than 0.");
        }
    }
}
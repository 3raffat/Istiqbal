using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Reservations.Commands.CreateReservation
{
    public sealed class CreateReservationCommandValidator:AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationCommandValidator()
        {
            RuleFor(x => x.GuestId)
           .NotEmpty().WithMessage("GuestId cannot be empty.");

            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("RoomId cannot be empty.");

            RuleFor(x => x.CheckInDate)
                .NotEmpty().WithMessage("Check-in date is required.")
                .Must(date => date >= DateTimeOffset.Now).WithMessage("Check-in date cannot be in the past.");

            RuleFor(x => x.CheckOutDate)
                .NotEmpty().WithMessage("Check-out date is required.")
                .GreaterThan(x => x.CheckInDate).WithMessage("Check-out date must be after check-in date.");
        }
    }
}

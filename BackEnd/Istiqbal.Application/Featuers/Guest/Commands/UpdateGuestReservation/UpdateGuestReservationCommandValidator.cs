using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Guest.Commands.UpdateGuestReservation
{
    public sealed class UpdateGuestReservationCommandValidator: AbstractValidator<UpdateGuestReservationCommand>
    {
        public UpdateGuestReservationCommandValidator()
        {
            RuleFor(x => x.GuestId)
           .NotEmpty().WithMessage("GuestId cannot be empty.");

            RuleFor(x => x.ReservationId)
                .NotEmpty().WithMessage("ReservationId cannot be empty.");

            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("RoomId cannot be empty.");

            RuleFor(x => x.CheckInDate)
                .NotEmpty().WithMessage("Check-in date is required.");

            RuleFor(x => x.CheckOutDate)
                .NotEmpty().WithMessage("Check-out date is required.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid reservation status.");
        }
    }
}

using FluentValidation;
using Istiqbal.Application.Featuers.Guest.Commands.DeleteGuestReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Guest.Commands.CancelGuestReservation
{
    public sealed class CancelGuestReservationCommandValidator:AbstractValidator<CancelGuestReservationCommand>
    {
        public CancelGuestReservationCommandValidator()
        {
            RuleFor(x => x.GuestId)
           .NotEmpty().WithMessage("GuestId cannot be empty.");

            RuleFor(x => x.ReservationId)
                .NotEmpty().WithMessage("ReservationId cannot be empty.");
        }
    }
}

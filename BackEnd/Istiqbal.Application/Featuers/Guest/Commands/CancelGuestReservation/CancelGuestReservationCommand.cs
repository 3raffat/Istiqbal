using Istiqbal.Application.Featuers.Reservations.Dtos;
using Istiqbal.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Guest.Commands.DeleteGuestReservation
{
    public sealed record CancelGuestReservationCommand(Guid guestId , Guid reservationId):IRequest<Result<ReservationDto>>;
   
}



using Istiqbal.Application.Featuers.Reservations.Dtos;
using Istiqbal.Domain.Common.Results;
using MediatR;
using static Istiqbal.Contracts.Requests.Reservation.CreateReservationRequest;

namespace Istiqbal.Application.Featuers.Reservations.Commands.CreateReservation
{
    public record CreateReservationCommand(
        Guid GuestId,
        Guid roomId,
        DateTimeOffset CheckInDate,
        DateTimeOffset CheckOutDate 
       ):IRequest<Result<ReservationDto>>;
   
}

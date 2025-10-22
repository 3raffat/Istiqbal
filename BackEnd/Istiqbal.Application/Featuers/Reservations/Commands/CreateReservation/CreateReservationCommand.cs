

using Istiqbal.Application.Featuers.Reservations.Dtos;
using Istiqbal.Domain.Common.Results;
using MediatR;
using static Istiqbal.Contracts.Requests.Reservation.CreateReservationRequest;

namespace Istiqbal.Application.Featuers.Reservations.Commands.CreateReservation
{
    public sealed record CreateReservationCommand(
        Guid GuestId,
        Guid RoomId,
        DateOnly CheckInDate,
        DateOnly CheckOutDate 
       ):IRequest<Result<ReservationDto>>;
   
}

using Istiqbal.Application.Featuers.Reservations.Dtos;
using Istiqbal.Domain.Common.Results;
using MediatR;
using static Istiqbal.Contracts.Requests.Reservation.CreateReservationRequest;

namespace Istiqbal.Application.Featuers.Guest.Commands.UpdateGuestReservation
{
    public sealed record UpdateGuestReservationCommand(
        Guid GuestId,
        Guid ReservationId,
        Guid roomId,
        DateTimeOffset CheckInDate,
        DateTimeOffset CheckOutDate,
        ReservationStatus Status):IRequest<Result<Updated>>;
}

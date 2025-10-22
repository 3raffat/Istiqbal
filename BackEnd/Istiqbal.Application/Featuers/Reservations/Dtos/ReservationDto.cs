using Istiqbal.Domain.Guestes.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Istiqbal.Contracts.Requests.Reservation.CreateReservationRequest;

namespace Istiqbal.Application.Featuers.Reservations.Dtos
{
    public sealed record  ReservationDto(
        Guid ReservationId,
        string GuestFullName,
        string Roomtype ,
        int RoomNumber,
        decimal Amount,
        DateOnly CheckInDate,
        DateOnly CheckOutDate,
        ReservationStatus Status);
   
}

using Istiqbal.Domain.Guestes.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Istiqbal.Contracts.Requests.Reservation.CreateReservationRequest;

namespace Istiqbal.Application.Featuers.Reservations.Dtos
{
    public record  ReservationDto(
        Guid reservationId,
        string guestFullName,
        string roomtype ,
        int roomNumber,
        decimal Amount,
        DateTimeOffset CheckInDate,
        DateTimeOffset CheckOutDate,
        ReservationStatus Status);
   
}

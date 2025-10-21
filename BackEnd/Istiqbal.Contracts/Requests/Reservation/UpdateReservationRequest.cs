using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Istiqbal.Contracts.Requests.Reservation.CreateReservationRequest;

namespace Istiqbal.Contracts.Requests.Reservation
{
    public sealed class UpdateReservationRequest
    {
        public Guid RoomId { get; set; }
        public DateTimeOffset CheckInDate { get; set; }
        public DateTimeOffset CheckOutDate { get; set; }
        public ReservationStatus Status { get; set; }
    }
}

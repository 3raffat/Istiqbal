using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Istiqbal.Contracts.Requests.Reservation
{
    public class CreateReservationRequest
    {
        public Guid GuestId { get; set; }
        public Guid RoomId { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum ReservationStatus
        {
            Pending,
            Confirmed,
            Cancelled,
            Completed
        }
    }
}

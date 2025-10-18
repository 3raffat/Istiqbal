using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Istiqbal.Contracts.Requests.Rooms
{
    public class updateRoomRequest
    {
        public Guid roomTypeId { get; set; }
        public RoomStatus roomStatus { get; set; }
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RoomStatus
    {
        Available,
        Occupied,
        UnderMaintenance,
        Cleaning
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Istiqbal.Contracts.Requests.Rooms
{
    public class UpdateRoomRequest
    {
        public Guid RoomTypeId { get; set; }
        public RoomStatus RoomStatus { get; set; }
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

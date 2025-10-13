using Istiqbal.Domain.Rooms.Amenities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Room.Dtos
{
    public sealed record  RoomDto (Guid id , int number, string roomType, int floor, string status, List<Amenity> amenities);
   
}

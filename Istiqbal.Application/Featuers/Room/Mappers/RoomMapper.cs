using Istiqbal.Application.Featuers.Room.Dtos;
using Istiqbal.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Room.Mappers
{
    public static class RoomMapper
    {
        //public static RoomDto ToDto(this Domain.Rooms.Room room)
        //{
        //    ArgumentNullException.ThrowIfNull(room);

        //    return new RoomDto(
        //        room.Id,
        //        room.Number,
        //        room.Type.Name,
        //        room.Floor,
        //        room.Status.ToString()
        //    );
        //}
        //public static List<RoomDto> ToDtos(this IEnumerable<Domain.Rooms.Room> rooms)
        //{
        //    return [..rooms.Select(x => x.ToDto())];
        //}
    }
}

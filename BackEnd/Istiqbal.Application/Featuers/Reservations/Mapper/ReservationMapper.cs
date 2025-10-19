using Istiqbal.Application.Featuers.Reservations.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Reservations.Mapper
{
    public static class ReservationMapper
    {

        public static ReservationDto toDto(this Domain.Guestes.Reservations.Reservation entity)
        {
            return new ReservationDto(
                entity.Id,
                entity.Guest.FullName,
                entity.Room.Type.Name,
                entity.Room.Number,
                entity.Amount,
                entity.CheckInDate,
                entity.CheckOutDate,
                entity.Status);
        }
        public static List<ReservationDto> toDtos(this List<Domain.Guestes.Reservations.Reservation> entity)
        {
            return [.. entity.Select(x => x.toDto())];
        }

    }
}

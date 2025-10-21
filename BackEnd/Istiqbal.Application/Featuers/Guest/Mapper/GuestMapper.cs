using Istiqbal.Application.Featuers.Guest.Dtos;
using Istiqbal.Application.Featuers.Reservations.Mapper;
using Istiqbal.Domain.Guestes.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Guest.Mapper
{
    public static class GuestMapper
    {

        public static GuestDto toDto(this Domain.Guestes.Guest guest)
        {
            return new GuestDto
            (
                Id: guest.Id,
                FullName: guest.FullName,
                Email: guest.Email,
                Phone: guest.Phone,
                Reservations: guest.Reservation.Select(x => x.toDto()).ToList()
            );
        }

        public static List<GuestDto> ToDtos(this List<Domain.Guestes.Guest> guests)
        {
            return[..guests.Select(x => toDto(x))];
        }
    }
}

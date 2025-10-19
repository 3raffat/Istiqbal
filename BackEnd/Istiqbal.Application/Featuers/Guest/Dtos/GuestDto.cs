using Istiqbal.Application.Featuers.Reservations.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Guest.Dtos
{
    public sealed record GuestDto(Guid Id , string fullName, string email, string phone , List<ReservationDto> Reservations);
   
}

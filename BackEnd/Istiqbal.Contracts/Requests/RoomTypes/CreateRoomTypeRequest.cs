using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Contracts.Requests.RoomTypes
{
    public sealed class CreateRoomTypeRequest
    {
        
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty ;

        public decimal PricePerNight { get; set; }

        public int MaxOccupancy { get; set; }
        public List<Guid> AmenitieIds  { get; set; } = new List<Guid> ();
    }
}

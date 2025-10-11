using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.RoomType.Dtos
{
    public sealed record RoomTypeDto(Guid Id, string Name, string Description, decimal PricePerNight, int MaxOccupancy);
    
      
   
}

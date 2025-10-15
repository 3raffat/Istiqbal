using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Contracts.Requests.Amenity
{
    public sealed class CreateAmenityRequest
    {
        public string name { get; set; } = string.Empty;    
    }
}

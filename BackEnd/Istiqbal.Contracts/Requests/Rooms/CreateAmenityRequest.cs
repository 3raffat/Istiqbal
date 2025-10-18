using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Contracts.Requests.Rooms
{
    public sealed class CreateAmenityRequest
    {
        public Guid id { get; set; }
        public string name { get; set; } = string.Empty;
    }
}

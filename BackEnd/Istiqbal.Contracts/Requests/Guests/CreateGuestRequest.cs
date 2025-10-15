using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Contracts.Requests.Guests
{
    public sealed class CreateGuestRequest
    {
        public string fullName { get; set; }=string.Empty;
        public string email { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;

    }
}

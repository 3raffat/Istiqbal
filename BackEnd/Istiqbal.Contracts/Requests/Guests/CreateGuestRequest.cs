using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Contracts.Requests.Guests
{
    public sealed class CreateGuestRequest
    {
        public string FullName { get; set; }=string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

    }
}

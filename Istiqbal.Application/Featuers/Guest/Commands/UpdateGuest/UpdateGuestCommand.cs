using Istiqbal.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Guest.Commands.UpdateGuest
{
    public sealed record UpdateGuestCommand(Guid id,string fullName, string phone, string email):IRequest<Result<Updated>>;
   
}

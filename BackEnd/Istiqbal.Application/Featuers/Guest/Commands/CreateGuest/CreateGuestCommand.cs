using Istiqbal.Application.Featuers.Guest.Dtos;
using Istiqbal.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Guest.Commands.CreateGuest
{
    public sealed record CreateGuestCommand(string FullName, string Phone, string Email) : IRequest<Result<GuestDto>>;
   
}

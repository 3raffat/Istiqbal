using Istiqbal.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Amenities.Commands.DeleteAmenity
{
    public sealed record DeleteAmenityCommand(Guid Id):IRequest<Result<Deleted>>;
   
}

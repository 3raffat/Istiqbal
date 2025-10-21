using Istiqbal.Application.Featuers.Amenity.Dtos;
using Istiqbal.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Amenity.Commands.UpdateAmenity
{
    public sealed record UpdateAmenityCommand(Guid Id , string Name):IRequest<Result<AmenityDto>>;
   
}

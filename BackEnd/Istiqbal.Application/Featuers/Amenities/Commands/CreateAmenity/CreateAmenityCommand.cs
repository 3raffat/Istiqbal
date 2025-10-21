using Istiqbal.Application.Featuers.Amenity.Dtos;
using Istiqbal.Domain.Common.Results;
using MediatR;


namespace Istiqbal.Application.Featuers.Amenity.Commands.CreateAmenity
{
    public sealed record CreateAmenityCommand(string Name):IRequest<Result<AmenityDto>>;
 
}

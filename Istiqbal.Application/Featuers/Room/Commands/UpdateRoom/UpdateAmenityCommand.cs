using Istiqbal.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Room.Commands.UpdateRoom
{
    public sealed record  UpdateAmenityCommand (Guid id, string name) : IRequest<Result<Updated>>;
    
}

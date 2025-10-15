using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Amenity.Commands.UpdateAmenity
{
    public sealed class UpdateAmenityCommandValidator :AbstractValidator<UpdateAmenityCommand>  
    {
        public UpdateAmenityCommandValidator()
        {
            RuleFor(x => x.id)
               .NotEmpty()
               .WithMessage("Amenity ID is required.");

            RuleFor(x => x.name)
                .NotEmpty()
                .WithMessage("Amenity name is required.");
        }
    }
}

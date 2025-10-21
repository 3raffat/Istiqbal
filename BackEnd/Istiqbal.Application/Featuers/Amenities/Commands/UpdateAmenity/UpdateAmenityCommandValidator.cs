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
            RuleFor(x => x.Id)
               .NotEmpty()
               .WithMessage("Amenity ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Amenity name is required.")
                .MaximumLength(50).WithMessage("The Name field must not exceed 50 characters.");
            ;
        }
    }
}

using Istiqbal.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Common.Errors
{
    public static class ApplicationErrors
    {
        public static Error RoomTypeNotFound =>
           Error.NotFound(
        "ApplicationErrors.RoomType.NotFound",
        "RoomType does not exist.");

    }
}

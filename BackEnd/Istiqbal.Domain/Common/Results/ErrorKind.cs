using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Istiqbal.Domain.Common.Results
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ErrorKind
    {
        Failure,
        Unexpected,
        Validation,
        Conflict,
        NotFound,
        Unauthorized,
        Forbidden,
    }
}

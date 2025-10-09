using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Domain.Common.Results.Abstraction
{
    public interface IResult
    {
        List<Error>? Errors { get; }

        bool IsSuccess { get; }
    }

    public interface IResult<out TValue> : IResult
    {
        TValue Value { get; }
    }
}

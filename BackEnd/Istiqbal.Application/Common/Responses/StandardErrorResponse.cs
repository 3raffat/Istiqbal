using Istiqbal.Domain.Common.Results;

namespace Istiqbal.Application.Common.Responses
{
    public sealed partial class StandardResponse
    {
        public sealed record StandardErrorResponse(string Timestamp,
                                                   int Status,
                                                   string Message,
                                                   string Error,
                                                   string TraceId);

    }
}

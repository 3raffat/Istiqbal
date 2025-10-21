namespace Istiqbal.Application.Common.Responses
{
    public sealed partial class StandardResponse
    {
        public sealed record StandardSuccessResponse<T>(T? Data,
                                                        int Status,
                                                        string Message);
    }
}

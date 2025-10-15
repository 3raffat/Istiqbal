
using Istiqbal.Domain.Common.Results;

namespace Istiqbal.Application.Common.Interface
{
    public interface IIdentityService
    {
        Task<Result<Success>> AssignRole(Guid userId,string roleName,CancellationToken cancellationToken);
    }
}

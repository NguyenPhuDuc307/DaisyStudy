using DaisyStudy.ViewModels.Common;
using DaisyStudy.ViewModels.System.Roles;

namespace DaisyStudy.ApiIntegration.System.Roles;

public interface IRoleApiClient
{
    Task<ApiResult<List<RoleViewModel>>> GetAll();
}
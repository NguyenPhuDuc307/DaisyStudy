using DaisyStudy.ViewModels.Common;
using DaisyStudy.ViewModels.System.Roles;

namespace DaisyStudy.AdminApp.Service
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleViewModel>>> GetAll();
    }
}
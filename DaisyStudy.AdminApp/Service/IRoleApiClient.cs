using DaisyStudy.ViewModels.System.Roles;
using eShopSolution.ViewModels.Common;

namespace DaisyStudy.AdminApp.Service
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleViewModel>>> GetAll();
    }
}
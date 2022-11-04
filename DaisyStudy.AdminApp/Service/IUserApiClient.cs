using DaisyStudy.ViewModels.Common;
using DaisyStudy.ViewModels.System.Roles;
using DaisyStudy.ViewModels.System.Users;
using eShopSolution.ViewModels.Common;

namespace DaisyStudy.AdminApp.Service;
public interface IUserApiClient
{
    Task<ApiResult<string>> Authenticate(LoginRequest request);
    Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request);
    Task<ApiResult<bool>> RegisterUser(RegisterRequest registerRequest);
    Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request);
    Task<ApiResult<UserViewModel>> GetById(Guid id);
    Task<ApiResult<bool>> Delete(Guid id);
    Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
}
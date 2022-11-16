using DaisyStudy.ViewModels.Common;
using DaisyStudy.ViewModels.System.Users;

namespace DaisyStudy.ApiIntegration.System.Users;
public interface IUserApiClient
{
    Task<ApiResult<string>> Authenticate(LoginRequest request);
    Task<ApiResult<string>> Login(LoginRequest request);
    Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request);
    Task<ApiResult<bool>> RegisterUser(RegisterRequest registerRequest);
    Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request);
    Task<ApiResult<UserViewModel>> GetById(Guid id);
    Task<ApiResult<UserViewModel>> GetByName(string UserName);
    Task<ApiResult<bool>> Delete(Guid id);
    Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
}
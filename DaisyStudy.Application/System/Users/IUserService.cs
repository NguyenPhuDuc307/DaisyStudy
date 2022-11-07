using DaisyStudy.ViewModels.Common;
using DaisyStudy.ViewModels.System.Users;

namespace DaisyStudy.Application.System.Users
{
    public interface IUserService{
        Task<ApiResult<string>> Authenticate(LoginRequest request);
        Task<ApiResult<string>> Login(LoginRequest request);
        Task<ApiResult<bool>> Register(RegisterRequest request);
        Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request);
        Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request);
        Task<ApiResult<UserViewModel>> GetById(Guid id);
        Task<ApiResult<bool>> Delete(Guid id);
        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }
    
}
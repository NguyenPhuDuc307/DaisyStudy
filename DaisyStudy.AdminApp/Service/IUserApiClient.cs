using DaisyStudy.ViewModels.Common;
using DaisyStudy.ViewModels.System.Users;

namespace DaisyStudy.AdminApp.Service;
public interface IUserApiClient
{
    Task<string> Authenticate(LoginRequest request);
    Task<PagedResult<UserViewModel>> GetUsersPaging(GetUserPagingRequest request);
}
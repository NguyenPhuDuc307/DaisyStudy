using DaisyStudy.ViewModels.Common;
using DaisyStudy.ViewModels.System.Users;

namespace DaisyStudy.Application.System.Users
{
    public interface IUserService{
        Task<string> Authenticate(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
        Task<PagedResult<UserViewModel>> GetUsersPaging(GetUserPagingRequest request);
    }
    
}
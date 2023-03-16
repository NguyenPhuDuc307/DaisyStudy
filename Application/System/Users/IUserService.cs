using DaisyStudy.Data;
using DaisyStudy.Models.Common;
using DaisyStudy.Models.System.Users;

namespace DaisyStudy.Application.System.Users
{
    public interface IUserService{
        Task<bool> DepositMoney(string UserName, decimal money);
        Task<string> Register(RegisterRequest request);
        Task<string> Update(string id, ApplicationUser request);
        Task<PagedResult<UserViewModel>> GetUsersPaging(GetUserPagingRequest request);
        Task<UserViewModel> GetById(string id);
        Task<UserViewModel> GetByName(string UserName);
        Task<UserViewModel> GetByEmail(string Email);
        Task<bool> Delete(string id);
    }
    
}
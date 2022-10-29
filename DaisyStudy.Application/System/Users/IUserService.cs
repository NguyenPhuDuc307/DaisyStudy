using DaisyStudy.ViewModel.System.Users;

namespace DaisyStudy.Application.System.Users
{
    public interface IUserService{
        Task<string> Authenticate(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
    }
    
}
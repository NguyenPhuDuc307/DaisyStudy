using DaisyStudy.ViewModels.System.Roles;

namespace DaisyStudy.Application.System.Roles
{
    public interface IRoleService
    {
        Task<List<RoleViewModel>> GetAll();
    }
}
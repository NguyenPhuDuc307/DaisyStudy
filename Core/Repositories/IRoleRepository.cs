using Microsoft.AspNetCore.Identity;

namespace DaisyStudy.Core.Repositories
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}

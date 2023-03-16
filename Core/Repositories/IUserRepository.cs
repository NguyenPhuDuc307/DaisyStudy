using DaisyStudy.Data;

namespace DaisyStudy.Core.Repositories;

public interface IUserRepository
{
    ICollection<ApplicationUser> GetUsers(string? keyword);

    Task<ApplicationUser?> GetUser(string id);

    ApplicationUser UpdateUser(ApplicationUser user);
}

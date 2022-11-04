using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.ViewModels.System.Users;
public class RoleAssignRequest
{
    public Guid Id { get; set; }
    public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
}
using DaisyStudy.Models.Common;

namespace DaisyStudy.Models.System.Users;
public class RoleAssignRequest
{
    public Guid Id { get; set; }
    public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
}
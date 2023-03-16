using DaisyStudy.Models.Common;

namespace DaisyStudy.Models.Catalog.Homeworks;

public class GetManageHomeworkPagingRequest : PagingRequestBase
{
    public string? Keyword { get; set; }
    public int ClassID { get; set; }
    public string? UserId { get; set; }
}
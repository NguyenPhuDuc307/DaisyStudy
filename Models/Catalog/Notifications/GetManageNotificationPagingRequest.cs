using DaisyStudy.Models.Common;

namespace DaisyStudy.Models.Catalog.Notifications;

public class GetManageNotificationPagingRequest : PagingRequestBase
{
    public string? Keyword { get; set; }
    public int ClassID { get; set; }
}
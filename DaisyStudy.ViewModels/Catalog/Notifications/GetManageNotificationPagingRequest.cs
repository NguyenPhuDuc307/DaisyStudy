using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.ViewModels.Catalog.Notifications;

public class GetManageNotificationPagingRequest : PagingRequestBase
{
    public string? Keyword { get; set; }
    public int ClassID { get; set; }
}
using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.ViewModels.Catalog.Comments;

public class GetManageCommentPagingRequest : PagingRequestBase
{
    public string? Keyword { get; set; }
    public int NotificationID { get; set; }
}
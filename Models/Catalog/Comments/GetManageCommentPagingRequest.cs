using DaisyStudy.Models.Common;

namespace DaisyStudy.Models.Catalog.Comments;

public class GetManageCommentPagingRequest : PagingRequestBase
{
    public string? Keyword { get; set; }
    public int NotificationID { get; set; }
}
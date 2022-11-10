using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.ViewModels.Catalog.Chats;

public class GetManageChatPagingRequest : PagingRequestBase
{
    public string? Keyword { get; set; }
    public int ClassID { get; set; }
}
using DaisyStudy.ViewModels.Catalog.Chats;
using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.ApiIntegration.Catalog.Chats;

public interface IChatApiClient
{
    Task<ApiResult<PagedResult<ChatViewModel>>> GetChatPaging(GetManageChatPagingRequest request);
    Task<bool> CreateChat(ChatCreateRequest request);
}
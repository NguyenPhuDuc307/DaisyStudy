using DaisyStudy.ViewModels.Catalog.Chats;
using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.Application.Catalog.Chats;

public interface IChatService
{
    Task<int> Create(ChatCreateRequest request);
    Task<int> Delete(int ChatID);
    Task<ApiResult<ChatViewModel>> GetById(int NotificationID);
    Task<ApiResult<PagedResult<ChatViewModel>>> GetAllPaging(GetManageChatPagingRequest request);
}
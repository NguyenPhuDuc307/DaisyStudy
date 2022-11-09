using DaisyStudy.ViewModels.Catalog.Notifications;
using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.Application.Catalog.Notifications;

public interface INotificationService
{
    Task<int> Create(NotificationCreateRequest request);
    Task<int> Update(NotificationUpdateRequest request);
    Task<int> Delete(int NotificationID);
    Task<ApiResult<NotificationViewModel>> GetById(int NotificationID);
    Task<ApiResult<PagedResult<NotificationViewModel>>> GetAllPaging(GetManageNotificationPagingRequest request);
}
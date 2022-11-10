using DaisyStudy.ViewModels.Catalog.Notifications;
using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.ApiIntegration.Catalog.Notifications;

public interface INotificationApiClient
{
    Task<ApiResult<PagedResult<NotificationViewModel>>> GetNotificationPaging(GetManageNotificationPagingRequest request);
    Task<bool> CreateNotification(NotificationCreateRequest request);
}
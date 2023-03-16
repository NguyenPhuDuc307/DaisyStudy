using DaisyStudy.Models.Catalog.Notifications;
using DaisyStudy.Models.Common;

namespace DaisyStudy.Application.Catalog.Notifications;

public interface INotificationService
{
    Task<int> Create(NotificationCreateRequest request);
    Task<int> Update(NotificationUpdateRequest request);
    Task<int> Delete(int NotificationID);
    Task<NotificationViewModel> GetById(int NotificationID);
    Task<PagedResult<NotificationViewModel>> GetAllPaging(GetManageNotificationPagingRequest request);
}
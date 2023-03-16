using AutoMapper;
using DaisyStudy.Data;
using DaisyStudy.Models.Catalog.Notifications;

namespace DaisyStudy.Models.Mappings;
public class NotificationProfile : Profile
{
    public NotificationProfile()
    {
        CreateMap<Notification, NotificationViewModel>();
        CreateMap<NotificationViewModel, NotificationUpdateRequest>();
    }
}
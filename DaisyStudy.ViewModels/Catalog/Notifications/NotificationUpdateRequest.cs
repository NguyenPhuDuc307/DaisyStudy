using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DaisyStudy.ViewModels.Catalog.Notifications;

public class NotificationUpdateRequest
{
    public int NotificationID { set; get; }

    [Display(Name = "Tiêu đề")]
    public string? Title { get; set; }

    [Display(Name = "Nội dung")]
    public string? Content { get; set; }

    [Display(Name = "Hình ảnh")]
    public IFormFile? ThumbnailImage { get; set; }
}

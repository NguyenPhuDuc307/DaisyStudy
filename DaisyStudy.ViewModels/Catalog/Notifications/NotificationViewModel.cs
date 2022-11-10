using System.ComponentModel.DataAnnotations;
using DaisyStudy.Data.Entities;
using DaisyStudy.ViewModels.Catalog.Comments;

namespace DaisyStudy.ViewModels.Catalog.Notifications;

public class NotificationViewModel
{
    public int NotificationID { set; get; }

    [Display(Name = "Mã lớp học")]
    public string? ClassID { set; get; }

    [Display(Name = "Tên lớp học")]
    public string? ClassName { set; get; }

    [Display(Name = "Tiêu đề")]
    public string? Title { get; set; }

    [Display(Name = "Nội dung")]
    public string? Content { get; set; }

    [Display(Name = "Ngày tạo")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime DateTimeCreated { set; get; }

    public List<NotificationImage>? NotificationImages { get; set; }

    public List<CommentViewModel>? Comments { get; set; }
}


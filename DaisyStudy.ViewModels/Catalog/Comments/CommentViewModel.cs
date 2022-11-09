using System.ComponentModel.DataAnnotations;
using DaisyStudy.Data.Entities;

namespace DaisyStudy.ViewModels.Catalog.Comments;

public class CommentViewModel
{
    public int CommentID { set; get; }
    public int NotificationID { set; get; }
    public Guid UserID { set; get; }

    [Display(Name = "Ảnh đại diện")]
    public string? Avatar { set; get; }

    [Display(Name = "Họ tên")]
    public string? FullName { set; get; }

    [Display(Name = "Nội dung")]
    public string? Content { set; get; }

    [Display(Name = "Ngày tạo")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime DateTimeCreated { set; get; }

    [Display(Name = "Số người thích")]
    public int Likes { set; get; }

    [Display(Name = "Số không thích")]
    public int Dislikes { set; get; }
    
    public List<CommentImage>? CommentImages { get; set; }
}


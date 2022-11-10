using System.ComponentModel.DataAnnotations;
using DaisyStudy.Data.Entities;

namespace DaisyStudy.ViewModels.Catalog.Chats;

public class ChatViewModel
{
    public int ChatID { set; get; }
    public int ClassID { set; get; }
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

    public int Likes { set; get; }
    public int Dislikes { set; get; }

    public List<ChatImage>? ChatImages { get; set; }
}


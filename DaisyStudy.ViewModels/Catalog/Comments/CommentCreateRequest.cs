using System.ComponentModel.DataAnnotations;
using DaisyStudy.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace DaisyStudy.ViewModels.Catalog.Comments;

public class CommentCreateRequest
{
    public string? ReturnUrl { get; set; }
    public int NotificationID { set; get; }
    public string? UserName { set; get; }

    [Display(Name = "Nội dung")]
    public string? Content { set; get; }

    public List<IFormFile>? CommentImages { get; set; }
}


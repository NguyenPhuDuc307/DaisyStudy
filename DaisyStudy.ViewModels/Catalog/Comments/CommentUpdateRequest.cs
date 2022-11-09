using System.ComponentModel.DataAnnotations;
using DaisyStudy.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace DaisyStudy.ViewModels.Catalog.Comments;

public class CommentUpdateRequest
{
    public int CommentID { set; get; }

    [Display(Name = "Nội dung")]
    public string? Content { set; get; }

    public List<IFormFile>? CommentImages { get; set; }
}

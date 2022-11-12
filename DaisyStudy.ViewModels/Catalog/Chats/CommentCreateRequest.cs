using System.ComponentModel.DataAnnotations;
using DaisyStudy.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace DaisyStudy.ViewModels.Catalog.Chats;

public class ChatCreateRequest
{
    public string? ReturnUrl { get; set; }
    public int ClassID { set; get; }
    public string? UserName { set; get; }

    [Display(Name = "Nội dung")]
    public string? Content { set; get; }

    public List<IFormFile>? ChatImages { get; set; }
}


using System.ComponentModel.DataAnnotations;
using DaisyStudy.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace DaisyStudy.ViewModels.Catalog.Chats;

public class ChatUpdateRequest
{
    public int ChatID { set; get; }

    [Display(Name = "Nội dung")]
    public string? Content { set; get; }

    public List<IFormFile>? ChatImages { get; set; }
}

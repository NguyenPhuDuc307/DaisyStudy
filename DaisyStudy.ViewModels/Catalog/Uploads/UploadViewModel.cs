
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DaisyStudy.ViewModels.Catalog.Uploads;

public class UploadViewModel
{
    [Required]
    public int RoomId { get; set; }
    [Required]
    public IFormFile? File { get; set; }
    public string? UserName { get; set; }
}

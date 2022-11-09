using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DaisyStudy.ViewModels.Catalog.Classes;

public class ClassImageUpdateRequest
{
    [Display(Name = "Hình ảnh")]
    public IFormFile? ThumbnailImage { get; set; }
}


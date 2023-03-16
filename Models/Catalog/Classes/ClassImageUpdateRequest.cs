using System.ComponentModel.DataAnnotations;

namespace DaisyStudy.Models.Catalog.Classes;

public class ClassImageUpdateRequest
{
    [Display(Name = "Hình ảnh")]
    public IFormFile? ThumbnailImage { get; set; }
}


using Microsoft.AspNetCore.Http;

namespace DaisyStudy.ViewModels.Catalog.ClassImages;

public class ClassImageCreateRequest
{
    public IFormFile? ImageFile { get; set; }
}


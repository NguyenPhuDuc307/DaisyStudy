using DaisyStudy.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace DaisyStudy.ViewModel.Catalog.ClassImages;

public class ClassImageCreateRequest
{
    public IFormFile ImageFile { get; set; }
}


using Microsoft.AspNetCore.Http;

namespace DaisyStudy.ViewModels.Catalog.ClassImages
{
    public class ClassImageUpdateRequest
    {
        public int Id { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
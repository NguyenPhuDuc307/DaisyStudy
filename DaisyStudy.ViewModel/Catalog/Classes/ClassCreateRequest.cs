using Microsoft.AspNetCore.Http;

namespace DaisyStudy.ViewModel.Catalog.Classes;

public class ClassCreateRequest
{
    public string ClassName { set; get; }
    public string Topic { set; get; }
    public string ClassRoom { set; get; }
    public string Description { set; get; }
    public string SEOClassName { set; get; }
    public string SEODescriptione { set; get; }
    public string SEOAlias { set; get; }
    public decimal Tuition { set; get; }
    public IFormFile ThumbnailImage { get; set; }
}


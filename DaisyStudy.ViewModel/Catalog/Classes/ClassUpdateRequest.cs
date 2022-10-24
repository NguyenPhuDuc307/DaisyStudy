using Microsoft.AspNetCore.Http;

namespace DaisyStudy.ViewModel.Catalog.Classes;

public interface ClassUpdateRequest
{
    public int ID { set; get; }
    public String ClassName { set; get; }
    public String Topic { set; get; }
    public String Image { set; get; }
    public String ClassRoom { set; get; }
    public String Description { set; get; }
    public String SEOClassName { set; get; }
    public String SEODescriptione { set; get; }
    public String SEOAlias { set; get; }
    public IFormFile ThumbnailImage { get; set; }
}


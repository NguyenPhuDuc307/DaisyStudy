using Microsoft.AspNetCore.Http;

namespace DaisyStudy.ViewModels.Catalog.Classes;

public class ClassUpdateRequest
{
public int ID { set; get; }
public string ClassName { set; get; }
public string Topic { set; get; }
public string ClassRoom { set; get; }
public string Description { set; get; }
public string SEOClassName { set; get; }
public string SEODescriptione { set; get; }
public string SEOAlias { set; get; }
public IFormFile ThumbnailImage { get; set; }
}

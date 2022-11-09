using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DaisyStudy.ViewModels.Catalog.Classes;

public class ClassUpdateRequest
{
public int ID { set; get; }

[Display(Name = "Tên lớp học")]
public string ClassName { set; get; }

[Display(Name = "Chủ đề")]
public string Topic { set; get; }

[Display(Name = "Phòng học")]
public string ClassRoom { set; get; }

[Display(Name = "Mô tả")]
public string Description { set; get; }

[Display(Name = "SEO tên lớp học")]
public string SEOClassName { set; get; }

[Display(Name = "SEO mô tả")]
public string SEODescriptione { set; get; }

[Display(Name = "SEO Alias")]
public string SEOAlias { set; get; }

[Display(Name = "Hình ảnh")]
public IFormFile ThumbnailImage { get; set; }
}

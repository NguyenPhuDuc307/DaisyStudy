using DaisyStudy.Data.Entities;

namespace DaisyStudy.ViewModels.Catalog.Classes;

public class ClassViewModel
{
    public Int32 ID { set; get; }
    public string ClassID { set; get; }
    public string ClassName { set; get; }
    public string Topic { set; get; }
    public string ClassRoom { set; get; }
    public string Description { set; get; }
    public string SEOClassName { set; get; }
    public string SEODescriptione { set; get; }
    public string SEOAlias { set; get; }
    public decimal Tuition { set; get; }
    public DateTime DateCreated { set; get; }
    public int ViewCount { set; get; }
    public Status Status { set; get; }
    public IsPublic isPublic { set; get; }
}


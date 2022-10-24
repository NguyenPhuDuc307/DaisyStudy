using System;
using DaisyStudy.Data.Entities;

namespace DaisyStudy.ViewModel.Catalog.Classes;

public class ClassViewModel
{
    public int ID { set; get; }
    public String Class_ID { set; get; }
    public String ClassName { set; get; }
    public String Topic { set; get; }
    public String Image { set; get; }
    public String ClassRoom { set; get; }
    public String Description { set; get; }
    public String SEOClassName { set; get; }
    public String SEODescriptione { set; get; }
    public String SEOAlias { set; get; }
    public decimal Tuition { set; get; }
    public DateTime DateCreated { set; get; }
    public int ViewCount { set; get; }
    public Status Status { set; get; }
    public IsPublic isPublic { set; get; }
}


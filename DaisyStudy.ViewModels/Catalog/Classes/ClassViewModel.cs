using System.ComponentModel.DataAnnotations;
using DaisyStudy.Data.Entities;

namespace DaisyStudy.ViewModels.Catalog.Classes;

public class ClassViewModel
{
    public Int32 ID { set; get; }

    [Display(Name = "Mã lớp học")]
    public string ClassID { set; get; }

    [Display(Name = "Tên lớp học")]
    public string ClassName { set; get; }

    [Display(Name = "Chủ đề")]
    public string Topic { set; get; }

    [Display(Name = "Phòng học")]
    public string ClassRoom { set; get; }

    [Display(Name = "Mô tả")]
    public string Description { set; get; }

    [Display(Name = "SEO class name")]
    public string SEOClassName { set; get; }

    [Display(Name = "SEO mô tả")]
    public string SEODescriptione { set; get; }

    [Display(Name = "SEO Alias")]
    public string SEOAlias { set; get; }

    [Display(Name = "Học phí")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N1}")]
    public decimal Tuition { set; get; }

    [Display(Name = "Ngày tạo")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime DateCreated { set; get; }

    [Display(Name = "Lượt xem")]
    public int ViewCount { set; get; }

    [Display(Name = "Trạng thái")]
    public Status Status { set; get; }

    [Display(Name = "Công khai")]
    public IsPublic isPublic { set; get; }
}


using System.ComponentModel.DataAnnotations;
using DaisyStudy.Data.Entities;

namespace DaisyStudy.ViewModels.Catalog.Homeworks;

public class HomeworkViewModel
{
    public int HomeworkID { set; get; }

    [Display(Name = "Mã lớp học")]
    public string? ClassID { set; get; }

    [Display(Name = "Tên lớp học")]
    public string? ClassName { set; get; }

    [Display(Name = "Tên bài tập")]
    public string? HomeworkName { set; get; }

    [Display(Name = "Mô tả")]
    public string? Description { set; get; }

    [Display(Name = "Ngày tạo")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime DateTimeCreated { set; get; }

    [Display(Name = "Hạn nộp bài")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime Deadline { set; get; }
}


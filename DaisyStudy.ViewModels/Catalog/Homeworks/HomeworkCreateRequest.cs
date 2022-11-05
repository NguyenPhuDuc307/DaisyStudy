using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DaisyStudy.ViewModels.Catalog.Homeworks;

public class HomeworkCreateRequest
{
    [Display(Name = "Mã lớp học")]
    public int ClassID { set; get; }

    [Display(Name = "Tên bài tập")]
    public string? HomeworkName { set; get; }

    [Display(Name = "Mô tả")]
    public string? Description { set; get; }

    [Display(Name = "Hạn nộp bài")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime Deadline { set; get; }
}


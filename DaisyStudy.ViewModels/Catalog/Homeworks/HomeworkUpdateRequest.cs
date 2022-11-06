using System.ComponentModel.DataAnnotations;

namespace DaisyStudy.ViewModels.Catalog.Homeworks;

public class HomeworkUpdateRequest
{
    public int HomeworkID { set; get; }

    [Display(Name = "Tên bài tập")]
    public string? HomeworkName { set; get; }

    [Display(Name = "Mô tả")]
    public string? Description { set; get; }

    [Display(Name = "Hạn nộp bài")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime Deadline { set; get; }
}

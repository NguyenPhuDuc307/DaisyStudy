using System.ComponentModel.DataAnnotations;

namespace DaisyStudy.ViewModels.Catalog.ExamSchedules;

public class ExamSchedulesUpdateRequest
{
    public int ExamScheduleID { set; get; }

    [Display(Name = "Tên kì thi")]
    public string? ExamScheduleName { set; get; }

    [Display(Name = "Ngày thi")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime ExamDatetime { set; get; }

    [Display(Name = "Thời gian thi")]
    public int ExamTime { set; get; }
}